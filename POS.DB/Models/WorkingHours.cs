using POS.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class WorkingHours
    {
        [Key]
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Relationships
        public int BusinessId { get; set; }

        [ForeignKey("BusinessId")]

        public Business Business { get; set; } // For company working hours

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } // For employee working hours
    }
}
