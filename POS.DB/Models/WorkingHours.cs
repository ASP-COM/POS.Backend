using POS.DB.Enums;
using System.ComponentModel.DataAnnotations;

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
        public Business Business { get; set; } // For company working hours
        public User Employee { get; set; } // For employee working hours
    }
}
