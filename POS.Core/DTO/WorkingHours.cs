using POS.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class WorkingHours
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int BusinessId { get; set; }
        public int EmployeeId { get; set; }

        public static explicit operator WorkingHours(DB.Models.WorkingHours v) => new WorkingHours
        {
            Id = v.Id,
            DayOfWeek = v.DayOfWeek,
            StartTime = v.StartTime,
            EndTime = v.EndTime,
            BusinessId = v.BusinessId,
            EmployeeId = v.EmployeeId,
        };
    }
}
