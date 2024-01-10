using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace POS.DB.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ResStart { get; set; }
        public DateTime ResEnd { get; set; }
        public bool? IsReserved { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User ProvidingEmployee { get; set; }
        
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Item Service { get; set; }

        public int? CustomerId { get; set;}
        [ForeignKey("CustomerId")]
        public User? Customer { get; set; }
    }
}
