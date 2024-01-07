using POS.Core.DTO;
using POS.Core.Utilities;
using POS.DB;

namespace POS.Core
{
    public interface IItemService
    {
        Item? CreateItem(CreateItemRequest request);

        Item UpdateItem(EditItemRequest request);

        Item GetItemById(int id);

        List<Item> GetAllItems();

        List<Item> GetAllItemsServices();

        List<Item> GetAllItemsByBusinessId(int  businessId);

        void DeleteItemById(int id);
    }
}
