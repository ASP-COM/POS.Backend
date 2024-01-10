using Microsoft.Identity.Client;

namespace POS.Core.DTO
{
    public class PayForReservationRequest
    {
        public int ReservationId { get; set; }
        public decimal TipAmount { get; set; }
        public string PaymentType { get; set; }
    }
}