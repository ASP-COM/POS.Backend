using POS.Core.DTO;
using POS.DB;
using POS.DB.Models;

namespace POS.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        public ReservationService(AppDbContext context) => _context = context;

        public Reservation? CreateReservationSlot(ReservationCreationRequest request) {
            // Get corresponding service
            var item = _context.Find<DB.Models.Item>(request.ServiceId);
            if (item == null || item.ServiceDuration == null) {
                return null;
            }
            
            // Get corresponding employee
            var employee = _context.Find<User>(request.EmployeeId);
            if (employee == null) {
                return null;
            }
            
            // Create reservation
            var reservation = new Reservation
            {
                Description = request.Description,
                ResStart = request.ResStart,
                ResEnd = request.ResStart.Add(item.ServiceDuration.Value),
                IsReserved = false,
                ProvidingEmployee = employee,
                Service = item
            };
            var tracking = _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return tracking.Entity;
        }

        // Claim a reservation slot as user
        public bool ClaimReservation(int reservationId, int userId) {
            var user = _context.Find<User>(userId);
            if (user == null) {
                return false;
            }

            var reservation = _context.Reservations.Find(reservationId);
            if (reservation == null || reservation.IsReserved == true) {
                return false;
            }

            reservation.Customer = user;
            reservation.IsReserved = true;
            _context.SaveChanges();
            return true;
        }

        // Release reservation slot
        public bool ReleaseReservation(int reservationId) {
            var reservation = _context.Reservations.Find(reservationId);
            if (reservation == null || reservation.IsReserved == false) {
                return false;
            }

            reservation.IsReserved = false;
            reservation.Customer = null;
            // FIXME: Check if this does anything
            _context.SaveChanges();
            return true;
        }

        public List<DateOnly> GetDatesWithFreeReservationsInRange(int? businessId, int? employeeId, int? serviceId, DateOnly start, DateOnly? end)
        {
            var start_dt = start.ToDateTime(TimeOnly.MinValue);
            var query = _context.Reservations.Where(i => i.IsReserved == false);
            if (employeeId != null) {
                query = query.Where(i => i.ProvidingEmployee.Id == employeeId);
            }
            if (serviceId != null) {
                query = query.Where(i => i.Service.Id == serviceId);
            }
            if (businessId != null) {
                query = query.Where(i => i.Service.BusinessId == businessId);
            }
            if (end != null) {
                var end_dt = end.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where(i => i.ResStart.Date >= start_dt.Date && i.ResStart.Date <= end_dt.Date);
            } else {
                query = query.Where(i => i.ResStart.Date == start_dt.Date);
            }

            return query.Select(i => i.ResStart.Date).Distinct().ToList().Select(i => DateOnly.FromDateTime(i)).ToList();
        }

        public List<Reservation> GetFreeReservationsStartingOnDate(int? employeeId, int? serviceId, DateOnly start)
        {
            var start_dt = start.ToDateTime(TimeOnly.MinValue).Date;
            var query = _context.Reservations.Where(i => i.IsReserved == false && i.ResStart.Date == start_dt);
            if (employeeId != null) {
                query = query.Where(i => i.ProvidingEmployee.Id == employeeId);
            }
            if (serviceId != null) {
                query = query.Where(i => i.Service.Id == serviceId);
            }

            return query.ToList();
        }

        public List<Reservation> GetUserReservations(int userId)
        {
            return _context.Reservations.Where(i => i.Customer != null && i.Customer.Id == userId).ToList();
        }

        public bool RemoveServation(int id)
        {
            var res = _context.Reservations.Find(id);
            if (res == null) {
                return false;
            }
            
            _context.Reservations.Remove(res);
            _context.SaveChanges();

            return true;
        }
    }
}
