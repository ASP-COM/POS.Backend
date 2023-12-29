using POS.Core.DTO;

namespace POS.Core
{
    public interface IVoucherService
    {
        Voucher CreateVoucher(CreateVoucherRequest request);
        Voucher EditVoucher(EditVoucherRequest request);
        void DeleteVoucherById(int voucherId);

        Voucher GetVoucherById(int id);
        //Voucher GetVoucherByOrderId(int orderId);
        List<Voucher> GetAllVouchers();
        List<Voucher> GetVouchersByBusinessId(int businessId);
    }
}
