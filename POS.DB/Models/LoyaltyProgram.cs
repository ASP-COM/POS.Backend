using System.ComponentModel.DataAnnotations;

namespace POS.DB.Models
{
    public class LoyaltyProgram
    {
        [Key]
        public int Id { get; set; }
        public Business Business { get; set; }
        public string Description {  get; set; }

        public List<LoyaltyCard>? LoyaltyCards { get; set; }
        public List<Discount>? Discounts { get; set; }   
    }
}
