namespace POS.Core.DTO
{
    public class CreateWorkingHoursRequest
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int BusinessId { get; set; }
        public int EmployeeId { get; set; }
    }
}
