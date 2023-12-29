using POS.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class EditVoucherRequest
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsUsed { get; set; }
        public decimal Amount { get; set; }
        public int? OrderId { get; set; }
        public int BusinessId { get; set; }
    }
}
