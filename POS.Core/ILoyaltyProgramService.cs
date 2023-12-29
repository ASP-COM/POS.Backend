using POS.DB.Models;

namespace POS.Core
{
    public interface ILoyaltyProgramService
    {
        List<LoyaltyProgram> GetLoyaltyPrograms();

        // Get all loyalty programs from a specific business
        List<LoyaltyProgram> GetBusinessAllLoyaltyPrograms(int businessId);
        LoyaltyProgram GetLoyaltyProgramById(int id);


        LoyaltyProgram CreateLoyaltyProgram(LoyaltyProgram business);
    }
}
