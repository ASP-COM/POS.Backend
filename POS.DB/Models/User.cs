using System.ComponentModel.DataAnnotations;

namespace POS.DB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}