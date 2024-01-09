namespace POS.Core.DTO
{
    public class CreateOrderLineRequest
    {
     
        public int UnitCount { get; set; }

        public int ItemId { get; set; }

        public int? TaxId { get; set; }
    }
}
