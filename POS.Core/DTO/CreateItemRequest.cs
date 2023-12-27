using POS.DB.Enums;

namespace POS.Core.DTO
{
    public class CreateItemRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } // WithoutTax
        public bool IsUnavailable { get; set; }
        public ItemType Type { get; set; }

        // New property for the amount of time the service will take
        public TimeSpan? ServiceDuration { get; set; }

        public int DefaultTaxId { get; set; }

        public int BusinessId { get; set; }

        public List<int> CategoryIds { get; set; } // Assuming we want to associate the item with specific categories

    }
}
