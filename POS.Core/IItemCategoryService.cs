using POS.Core.DTO;

namespace POS.Core
{
    public interface IItemCategoryService
    {
        List<ItemCategory> GetItemCategories();
        List<ItemCategory> GetItemCategoriesByBusinessId(int businessId);

        ItemCategory GetItemCategoryById(int itemCategoryId);

        ItemCategory CreateItemCategory(CreateItemCategoryRequest request);
    }
}
