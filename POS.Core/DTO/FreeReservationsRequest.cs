namespace POS.Core.DTO
{
    public class FreeReservationsRequest
    {
        public int? EmployeeId {get; set;}
        public int? ServiceId {get; set;}
        public DateOnly Start {get; set;}
    }
}