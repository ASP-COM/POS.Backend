using POS.Core.DTO;

namespace POS.Core
{
    public interface IDiscountService
    {
        Discount CreateDiscount(CreateDiscountRequest request);

        Discount UpdateDiscount(EditDiscountRequest request);

        Discount GetDiscountById(int discountId);

        List<Discount> GetAllDiscounts();

        void DeleteDiscount(int discountId);
    }
}
