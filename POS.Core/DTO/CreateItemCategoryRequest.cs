namespace POS.Core.DTO
{
    public class CreateItemCategoryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? BusinessId { get; set; }
    }
}
