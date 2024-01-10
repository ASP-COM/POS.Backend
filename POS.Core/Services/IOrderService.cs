using POS.Core.DTO;
using POS.DB.Models;

namespace POS.Core.Services
{
    public interface IOrderService
    {
        InvoiceResponse? CreateNewOrder(CreateOrderRequest createOrderRequest);
        InvoiceResponse? AddAdditionalItems(int id, List<CreateOrderLineRequest> orderLineRequest);


        InvoiceResponse? RemoveItems(int orderId, List<int> itemIds);

        InvoiceResponse? ApplyVoucher(int orderId, int voucherId);

        InvoiceResponse? PayForOrder(int orderId, string paymentType);
        InvoiceResponse? AddTip(int orderId, decimal tipAmount);

        InvoiceResponse? GetOrderInvoice(int orderId);
        bool CancelOrder(int orderId);
    }
}
