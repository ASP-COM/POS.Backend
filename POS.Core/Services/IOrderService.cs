using POS.Core.DTO;
using POS.DB.Models;

namespace POS.Core.Services
{
    public interface IOrderService
    {
        Order? CreateNewOrder(CreateOrderRequest createOrderRequest);
        Order? AddAdditionalItems(int id, List<CreateOrderLineRequest> orderLineRequest);

        bool CancelOrder(int orderId);

        bool RemoveItems(int orderId, List<int> itemIds);

        bool ApplyVoucher(int orderId, int voucherId);

        bool PayForOrder(int orderId, string paymentType);
        bool AddTip(int orderId, decimal tipAmount);

    }
}
