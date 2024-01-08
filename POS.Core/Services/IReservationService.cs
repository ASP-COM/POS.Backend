using POS.DB.Models;

namespace POS.Core.Services
{
    public interface IReservationService
    {
        /* Adds reservation. Returns reservation with assigned Id */
        public Reservation? CreateReservationSlot(DTO.ReservationCreationRequest request);
        public bool ClaimReservation(int reservationId, int userId);
        public bool ReleaseReservation(int reservationId);
        List<Reservation> GetUserReservations(int userId);
        List<DateOnly> GetDatesWithFreeReservationsInRange(int? employeeId, int? serviceId, DateOnly start, DateOnly? end);
        List<Reservation> GetFreeReservationsStartingOnDate(int? employeeId, int? serviceId, DateOnly start);
        bool RemoveServation(int id);
    }
}
