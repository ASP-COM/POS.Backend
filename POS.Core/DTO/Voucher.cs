namespace POS.Core.DTO
{
    public class Voucher
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsUsed { get; set; }
        public decimal Amount { get; set; }
        public int? OrderId { get; set; }
        public int BusinessId { get; set; }

        public static explicit operator Voucher(DB.Models.Voucher v) => new Voucher
        {
            Id = v.Id,
            Description = v.Description,
            ValidFrom = v.ValidFrom,
            ValidTo = v.ValidTo,
            IsUsed = v.IsUsed,
            Amount = v.Amount,
            OrderId = v.OrderId,
            BusinessId = v.BusinessId,
        };
    }
}
