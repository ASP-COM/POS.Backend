using POS.DB.Enums;
using POS.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } // WithoutTax
        public bool IsUnavailable { get; set; }
        public ItemType Type { get; set; }

        // New property for the amount of time the service will take
        public TimeSpan? ServiceDuration { get; set; }

        public int DefaultTaxId { get; set; }

        public int BusinessId { get; set; }

        // Include a property to represent the category information
        public List<string> CategoryNames { get; set; }

        public static explicit operator Item(DB.Models.Item v) => new Item
        {
            Id = v.Id,
            Name = v.Name,
            Description = v.Description,
            Price = v.Price,
            Type = v.Type,
            ServiceDuration = v.ServiceDuration,
            DefaultTaxId = v.DefaultTaxId,
            BusinessId = v.BusinessId,

            // Populate category names based on the associated categories
            CategoryNames = v.Categories?.Select(c => c.Name).ToList(),
        };
    }
}
