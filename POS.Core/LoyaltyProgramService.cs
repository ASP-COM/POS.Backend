using Microsoft.EntityFrameworkCore;
using POS.DB;
using POS.DB.Models;

namespace POS.Core
{
    public class LoyaltyProgramService : ILoyaltyProgramService
    {
        private readonly AppDbContext _context;

        public LoyaltyProgramService(AppDbContext context)
        {
            _context = context;
        }

        public LoyaltyProgram CreateLoyaltyProgram(LoyaltyProgram loyaltyProgram)
        {
            // Ensure that the businessId is provided
            if (loyaltyProgram.BusinessId == null || loyaltyProgram.BusinessId <= 0)
            {
                throw new InvalidOperationException("BusinessId must be provided for the loyalty program.");
            }

            // If Business object is not provided, fetch it by BusinessId
            if (loyaltyProgram.Business == null)
            {
                loyaltyProgram.Business = _context.Businesss.Find(loyaltyProgram.BusinessId);
            }

            // Add the loyalty program to the context
            _context.LoyaltyPrograms.Add(loyaltyProgram);

            // Save changes to the database
            _context.SaveChanges();

            return loyaltyProgram;
        }


        public List<LoyaltyProgram> GetBusinessAllLoyaltyPrograms(int businessId) => _context.LoyaltyPrograms.Where(l => l.Business.Id == businessId).ToList();

        public LoyaltyProgram GetLoyaltyProgramById(int id) => _context.LoyaltyPrograms.FirstOrDefault(l => l.Id == id);

        public List<LoyaltyProgram> GetLoyaltyPrograms()
        {
            return _context.LoyaltyPrograms
        //.Include(lp => lp.Business) // Eager loading - by default EF doesnt automatically load navigation properties
        .ToList();
        }
    }
}
