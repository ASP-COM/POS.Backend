using POS.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class CreateVoucherRequest
    {
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal Amount { get; set; }
        public int BusinessId { get; set; }
    }
}
