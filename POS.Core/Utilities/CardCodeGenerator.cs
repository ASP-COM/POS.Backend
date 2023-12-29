using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Utilities
{
    public static class CardCodeGenerator
    {
        // The generated card code structure contains: <issued at time> + <user's name first letter (Uppered)> + <user id> + dot . <total user card quantity)> + dot . <applied loyatly program Id>  
        public static string GenerateCardCode(DateTime iat, string userName, int userId, int cardQuantity, int loyaltyProgramId)
        {
            // Extract the year from the iat timestamp
            string yearPart = iat.Year.ToString().Substring(2, 2);

            // Extract the month and day from the iat timestamp
            string monthDayPart = iat.ToString("MMdd");

            // Extract the first letter of the user's name
            string nameInitial = userName.Substring(0, 1).ToUpper();

            // Combine the components to form the CardCode
            string cardCode = $"{yearPart}{monthDayPart}{nameInitial}{userId}.{cardQuantity}.{loyaltyProgramId}";

            return cardCode;

        }

    }
}
