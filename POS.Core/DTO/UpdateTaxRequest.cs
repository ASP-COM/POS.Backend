namespace POS.Core.DTO
{
    public class UpdateTaxRequest
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public decimal AmountPct { get; set; }
    }
}
