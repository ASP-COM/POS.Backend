using POS.Core.DTO;
using POS.Core.Utilities;
using POS.DB;

namespace POS.Core
{
    public class DiscountService : IDiscountService
    {
        public readonly AppDbContext _context;

        public DiscountService(AppDbContext context) 
        {  
            _context = context; 
        }

        public Discount CreateDiscount(CreateDiscountRequest request)
        {
            if(request.ValidFrom > request.ValidUntil)
            {
                // Handle the case where invalid date was entered
                // You can throw an exception or handle it based on your application's logic
                throw new InvalidOperationException("Inappropriate date entered error");
            }

            var newDiscount = new DB.Models.Discount
            {
                Name = request.Name,
                Description = request.Description,
                ValidFrom = request.ValidFrom,
                ValidUntil = request.ValidUntil,
                minQuantity = request.MinQuantity,
                DiscountInPct = request.Percentage,
                DiscountInAmount = request.FixedAmount,
            };

            // Fetch related entities based on relationships
            if (request.SpecificItemId > 0)
            {
                newDiscount.ForSpecificItem = _context.Items.Find(request.SpecificItemId);
            }

            _context.Discounts.Add(newDiscount);
            _context.SaveChanges();

            return (Discount)newDiscount;

        }

        public void DeleteDiscount(int discountId)
        {
            var discount = _context.Discounts.Find(discountId);

            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                _context.SaveChanges();
            }
        }

        public List<Discount> GetAllDiscounts() => 
            _context.Discounts
                .Select(d => (Discount)d)
                .ToList();

        public Discount GetDiscountById(int discountId) =>
            _context.Discounts
            .Where(d => d.Id == discountId)
            .Select(d => (Discount)d)
            .First();

        public Discount UpdateDiscount(EditDiscountRequest request)
        {
            var existingDiscount = _context.Discounts.Find(request.Id);

            if(existingDiscount != null)
            {
                // Map Properties from the request to update the Discount entity
                existingDiscount.Name = request.Name;
                existingDiscount.Description = request.Description;
                existingDiscount.ValidFrom = request.ValidFrom;
                existingDiscount.ValidUntil = request.ValidUntil;

                existingDiscount.minQuantity = request.MinQuantity;
                existingDiscount.DiscountInPct = request.Percentage;
                existingDiscount.DiscountInAmount = request.FixedAmount;
            }

            _context.SaveChanges();

            return (Discount)existingDiscount;
        }
    }
}
