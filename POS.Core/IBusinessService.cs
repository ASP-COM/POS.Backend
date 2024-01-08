using POS.Core.DTO;
using POS.DB.Models;

namespace POS.Core
{
    public interface IBusinessService
    {
        List<Business> GetBusinesses();
        Business GetBusinessById(int id);
        Business GetBusinessByName(string name);
        Business CreateBusiness(string businessName);
        List <EmployeeInfo>? GetBusinessEmployees(int id);
    }
}
