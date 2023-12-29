using POS.Core.DTO;

namespace POS.Core
{
    public interface ILoyaltyCardService
    {
        List<LoyaltyCard> GetLoyaltyCards();
        List<LoyaltyCard> GetLoyaltyCardsByUserId(int userId);
        List<LoyaltyCard> GetLoyaltyCardsByLoyaltyProgramId(int loyaltyProgramId);

        LoyaltyCard GetLoyaltyCardById(int id);
        LoyaltyCard GetLoyaltyCardByCardCode(string cardCode);

        int GetUserCardCount(int userId);

        LoyaltyCard CreateLoyaltyCard(CreateLoyaltyCardRequest loyaltyCard);

    }
}
