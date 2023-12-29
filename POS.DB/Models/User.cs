using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; } 
        public List<LoyaltyCard>? LoyaltyCards { get; set; }

        // Only for employees accounts
        public int? BusinessId { get; set; }

        [ForeignKey("BusinessId")]

        public Business? Business { get; set; }

        public List<WorkingHours>? EmployeeWorkingHours { get; set; }

        public List<Order>? Orders { get; set; } // Navigation property to orders

    }
}