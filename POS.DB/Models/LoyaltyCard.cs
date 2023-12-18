using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class LoyaltyCard
    {
        public int Id { get; set; }

        // Remove [Key] attribute from individual properties (entity framework requires only one key, thus, we need to configure a composite key in OnModelCreating)
        public string CardCode {  get; set; }
        // public int LoyaltyId {  get; set; } // the table already has LoyaltyId property

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User {  get; set; }

        public int LoyaltyPoints { get; set; }

        public int? LoyaltyProgramId { get; set; }

        [ForeignKey("LoyaltyProgramId")]
        public LoyaltyProgram? LoyaltyProgram { get; set; }

    }
}
