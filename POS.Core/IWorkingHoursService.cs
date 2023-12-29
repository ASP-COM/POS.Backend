using POS.Core.DTO;

namespace POS.Core
{
    public interface IWorkingHoursService
    {
        WorkingHours CreateWorkingHours(CreateWorkingHoursRequest request);

        WorkingHours GetWorkingHoursById(int id);
        List <WorkingHours> GetAllWorkingHours();
        List <WorkingHours> GetWorkingHoursByBusinessId(int businessId);
        List <WorkingHours> GetWorkingHoursByEmployeeId(int userId);

        void DeleteWorkingHoursById(int id);
    }
}
