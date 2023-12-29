using POS.Core.DTO;
using POS.DB;

namespace POS.Core
{
    public class WorkingHoursService : IWorkingHoursService
    {
        private readonly AppDbContext _context;
        public WorkingHoursService(AppDbContext context)
        {
            _context = context;
        }

        public WorkingHours CreateWorkingHours(CreateWorkingHoursRequest request)
        {
            var newWorkingHours = new DB.Models.WorkingHours
            {
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
            };

            // fetch related entities based on relationships
            if(request.BusinessId > 0)
            {
                newWorkingHours.Business = _context.Businesss.Find(request.BusinessId);
                if(newWorkingHours.Business != null)
                {
                    newWorkingHours.BusinessId = request.BusinessId;
                }
            }

            if(request.EmployeeId > 0)
            {
                newWorkingHours.Employee = _context.Users.Find(request.EmployeeId);
                if( newWorkingHours.Employee != null)
                {
                    newWorkingHours.Employee.Id = request.EmployeeId;
                }
            }

            _context.WorkingHours.Add(newWorkingHours);
            _context.SaveChanges();

            return (WorkingHours)newWorkingHours;
        }

        public void DeleteWorkingHoursById(int id)
        {
            var workingHours = _context.WorkingHours.Find(id);

            if (workingHours != null)
            {
                _context.WorkingHours.Remove(workingHours);
                _context.SaveChanges();
            }
        }

        public List<WorkingHours> GetAllWorkingHours() =>
            _context.WorkingHours
                .Select(w => (WorkingHours)w)
                .ToList();

        public List<WorkingHours> GetWorkingHoursByBusinessId(int businessId) =>
            _context.WorkingHours
                .Where(w => w.BusinessId == businessId)
                .Select(w => (WorkingHours)w)
                .ToList();

        public WorkingHours GetWorkingHoursById(int id) =>
            _context.WorkingHours
                .Where(w => w.Id == id)
                .Select(w => (WorkingHours)w)
                .First();


        public List<WorkingHours> GetWorkingHoursByEmployeeId(int employeeId) =>
            _context.WorkingHours
                .Where(w => w.EmployeeId == employeeId)
                .Select(w => (WorkingHours)w)
                .ToList();
    }
}
