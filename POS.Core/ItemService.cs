using Microsoft.EntityFrameworkCore;
using POS.Core.DTO;
using POS.Core.Utilities;
using POS.DB;

namespace POS.Core
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _context;
        public ItemService(AppDbContext context)
        {
            _context = context;
        }

        public Item CreateItem(CreateItemRequest request)
        {
            var newItem = new DB.Models.Item
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsUnavailable = false,
                Type = request.Type,

                ServiceDuration = request.ServiceDuration,
            };

            // Fetch related entities based on relationships
            if (request.BusinessId > 0)
            {
                newItem.Business = _context.Businesss.Find(request.BusinessId);
                if (newItem.Business != null)
                {
                    newItem.BusinessId = request.BusinessId;
                }
            }

            if (request.DefaultTaxId > 0)
            {
                newItem.DefaultTax = _context.Tax.Find(request.DefaultTaxId);
                if (newItem.DefaultTax != null)
                {
                    newItem.DefaultTaxId = request.DefaultTaxId;
                }
            }

            // Associate the item with categories
            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                newItem.Categories = _context.ItemCategories
                    .Where(category => request.CategoryIds.Contains(category.Id))
                    .ToList();
            }

            _context.Items.Add(newItem);
            _context.SaveChanges();

            return (Item)newItem;
        }

        public void DeleteItemById(int id)
        {
            var item = _context.Items.Find(id);

            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }

        public List<Item> GetAllItems() =>
            _context.Items
                .Include(i => i.Categories) // Ensure Categories are loaded to be able to preview CategoryNames
                .Select(i => (Item)i)
                .ToList();

        public List<Item> GetAllItemsByBusinessId(int businessId) =>
            _context.Items
                .Include(i => i.Categories) // Ensure Categories are loaded to be able to preview CategoryNames
                .Where(i => i.BusinessId == businessId)
                .Select(i => (Item)i)
                .ToList();

        public List<Item> GetAllItemsServices() =>
            _context.Items
                .Include(i => i.Categories)
                .Where(i => i.Type == POS.DB.Enums.ItemType.Service)
                .Select(i => (Item)i)
                .ToList();

        public Item UpdateItem(EditItemRequest request)
        {
            var existingItem = _context.Items
                .Include(i => i.Categories)
                .First(i => i.Id == request.Id);

            if (existingItem != null)
            {
                existingItem.Name = request.Name;
                existingItem.Description = request.Description;
                existingItem.Price = request.Price;
                existingItem.IsUnavailable = request.IsUnavailable;
                existingItem.Type = request.Type;

                existingItem.ServiceDuration = request.ServiceDuration;

                // Update relationships
                if (request.BusinessId > 0)
                {
                    existingItem.Business = _context.Businesss.Find(request.BusinessId);
                    existingItem.BusinessId = request.BusinessId;
                }

                if (request.DefaultTaxId > 0)
                {
                    existingItem.DefaultTax = _context.Tax.Find(request.DefaultTaxId);
                    existingItem.DefaultTaxId = request.DefaultTaxId;
                }

                // Add new associations
                if (request.CategoryIds != null && request.CategoryIds.Any())
                {
                    foreach(var categoryId in request.CategoryIds)
                    {
                        var category = _context.ItemCategories.Find(categoryId);
                        if(category != null && !existingItem.Categories.Any(c => c.Id == categoryId))
                        {
                            existingItem.Categories.Add(category);
                        }
                    }
                }

                // Remove missing associations
                var categoriesToRemove = existingItem.Categories
                    .Where(c => !request.CategoryIds.Contains(c.Id))
                    .ToList();

                foreach(var categoryToRemove in categoriesToRemove)
                {
                    existingItem.Categories.Remove(categoryToRemove);
                }

                _context.SaveChanges();
            }

            return (Item)existingItem;
        }

        public Item GetItemById(int id) =>
            _context.Items
                .Where(i => i.Id == id)
                .Select(i => (Item)i)
                .First();
    }
}
