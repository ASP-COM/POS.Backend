namespace POS.Core.DTO
{
    public class CreateOrderRequest
    {
        public DateTime? CreationDate { get; set; }
        public decimal TipAmount { get; set; }

        public int? CustomerId { get; set; } 
        public int? EmployeeId { get; set; } 

        public List<CreateOrderLineRequest>? OrderLines { get; set; }

    }
}
