using POS.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class ReservationClaimRequest
    {
        public int reservationId { get; set; }
        // FIXME: Should be gotten from token instead
        public int userId { get; set; }
    }
}
