using POS.Core.DTO;
using POS.DB;
using POS.DB.Models;

namespace POS.Core
{
    public class BusinessService : IBusinessService
    {
        private readonly AppDbContext _context;

        public BusinessService(AppDbContext context)
        {
            _context = context;
        }

        public Business CreateBusiness(string businessName)
        {
            var business = new Business
            {
                Name = businessName
            };

            var track = _context.Businesss.Add(business);
            _context.SaveChanges();

            return track.Entity;
        }

        public Business GetBusinessById(int id) => _context.Businesss.FirstOrDefault(b => b.Id == id);

        public Business GetBusinessByName(string name) => _context.Businesss.FirstOrDefault(b => b.Name == name);

        public List<EmployeeInfo> GetBusinessEmployees(int id)
        {
            return _context.Users.Where(u => u.BusinessId == id).Select(u => new EmployeeInfo
            {
                Id = u.Id,
                Name = u.Name
            }).ToList();
        }

        public List<ServiceInfo> GetBusinessServices(int id)
        {
            return _context.Items.Where(u => u.BusinessId == id && u.Type == DB.Enums.ItemType.Service).Select(u => new ServiceInfo
            {
                Id = u.Id,
                Name = u.Name,
                Price = u.Price,
                Duration = u.ServiceDuration.Value,
            }).ToList();
        }

        public List<Business> GetBusinesses()
        {
            return _context.Businesss.ToList();
        }
    }
}
