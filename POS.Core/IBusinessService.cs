using POS.DB.Models;

namespace POS.Core
{
    public interface IBusinessService
    {
        List<Business> GetBusinesses();
        Business GetBusinessById(int id);
        Business GetBusinessByName(string name);

        Business CreateBusiness(Business business);
    }
}
