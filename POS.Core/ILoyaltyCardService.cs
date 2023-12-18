using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using POS.Core.DTO;
using POS.DB.Models;

namespace POS.Core
{
    public interface ILoyaltyCardService
    {
        List<LoyaltyCard> GetLoyaltyCards();
        List<LoyaltyCard> GetLoyaltyCardsByUserId(int userId);
        List<LoyaltyCard> GetLoyaltyCardsByLoyaltyProgramId(int loyaltyProgramId);

        LoyaltyCard GetLoyaltyCardById(int id);
        LoyaltyCard GetLoyaltyCardByCardCode(string cardCode);

        LoyaltyCard CreateLoyaltyCard(CreateLoyaltyCardRequest loyaltyCard);

    }
}
