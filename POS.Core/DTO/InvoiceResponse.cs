using System.ComponentModel.DataAnnotations.Schema;
using POS.DB.Enums;

namespace POS.Core.DTO
{
    // FIXME: Maybe also add human readable names here

    public class InvoiceItem
    {
        public decimal UnitPrice { get; set; }
        public int UnitCount { get; set; }
        public decimal AppliedDiscount { get; set; }
        public decimal AppliedTax { get; set; }
        public int ItemId { get; set; }
    }
    public class InvoiceVoucher
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }

    public class InvoiceResponse
    {
        public int Id { get; set; }
        public decimal TotalSum { get; set; }
        public decimal TipAmount { get; set; }
        public OrderStatus Status {  get; set; }    
        public DateTime? PaidDate { get; set; } 
        public PaymentMethod PaymentMethod { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? BusinessId { get; set; }

        public decimal PayedByVouchersAmount { get; set; }
        public List<InvoiceItem>? InvoiceItems { get; set; }
        public List<InvoiceVoucher>? InvoiceVouchers { get; set; }
    }
}
