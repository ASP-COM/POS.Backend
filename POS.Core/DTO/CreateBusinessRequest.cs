using POS.DB.Models;

namespace POS.Core.DTO
{
    public class CreateBusinessRequest
    {
        public string BusinessName { get; set; }
        // FIXME: Maybe add business hours as an optional
    }
}