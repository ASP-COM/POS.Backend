using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class LoyaltyCard
    {
        [Key]
        public string CardCode {  get; set; }

        [Key]
        public int LoyaltyId {  get; set; }

        [ForeignKey("UserId")]
        public User User {  get; set; }

        public int LoyaltyPoints { get; set; } 

    }
}
