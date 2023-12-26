using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? BusinessId { get; set; }

        public static explicit operator ItemCategory(DB.Models.ItemCategory i) => new ItemCategory
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            BusinessId = i.BusinessId,
        };
    }
}
