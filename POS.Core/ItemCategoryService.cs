using POS.Core.DTO;
using POS.Core.Utilities;
using Microsoft.AspNetCore.Http;
using POS.DB;

namespace POS.Core
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly AppDbContext _context;

        public ItemCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public ItemCategory CreateItemCategory(CreateItemCategoryRequest request)
        {
            var newItemCategory = new DB.Models.ItemCategory
            {
                Name = request.Name,
                Description = request.Description,
            };

            // Fetch related entities based on relationships
            if(request.BusinessId > 0)
            {
                newItemCategory.Business = _context.Businesss.Find(request.BusinessId);
                if(newItemCategory.Business != null)
                {
                    newItemCategory.BusinessId = request.BusinessId;
                }
            }

            _context.ItemCategories.Add(newItemCategory);
            _context.SaveChanges();

            return (ItemCategory)newItemCategory;
        }

        public List<ItemCategory> GetItemCategories() =>
            _context.ItemCategories
                .Select(i => (ItemCategory)i)
                .ToList();

        public List<ItemCategory> GetItemCategoriesByBusinessId(int businessId) =>
            _context.ItemCategories
                .Where(i => i.Business.Id == businessId)
                .Select(i => (ItemCategory)i)
                .ToList();

        public ItemCategory GetItemCategoryById(int itemCategoryId) =>
            _context.ItemCategories
                .Where(i => i.Id == itemCategoryId)
                .Select(i => (ItemCategory)i)
                .First();
        }
}
