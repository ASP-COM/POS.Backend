using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class LoyaltyCard
    {
        public int Id { get; set; }
        public int LoyaltyPoints { get; set; }
        public string CardCode { get; set; }

        public int? LoyaltyProgramId { get; set; }

        public int UserId { get; set; }


        public static explicit operator LoyaltyCard(DB.Models.LoyaltyCard l) => new LoyaltyCard
        {
            Id = l.Id,
            LoyaltyPoints = l.LoyaltyPoints,
            CardCode = l.CardCode,
            
            LoyaltyProgramId = l.LoyaltyProgramId,
            UserId = l.UserId,
        };
    }
}
