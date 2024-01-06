using POS.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    // Create a reservable slot in the system
    // To create this, we need description, service, start time and employee
    public class ReservationCreationRequest
    {
        public string Description { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set;}
        public DateTime ResStart { get; set; }
    }
}
