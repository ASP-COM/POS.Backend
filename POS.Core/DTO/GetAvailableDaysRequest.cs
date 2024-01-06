using POS.DB.Models;

namespace POS.Core.DTO
{
    public class GetAvailableDaysRequest
    {
        public int? employeeId {get; set;}
        public int? serviceId {get; set;}
        public DateOnly start {get; set;}
        public DateOnly? end {get; set;}
    }
}