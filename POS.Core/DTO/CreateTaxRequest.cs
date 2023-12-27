namespace POS.Core.DTO
{
    public class CreateTaxRequest
    {
        public string? Description { get; set; }

        public decimal AmountPct { get; set; }
    }
}
