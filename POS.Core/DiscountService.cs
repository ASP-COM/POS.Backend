using Microsoft.EntityFrameworkCore;
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

            // Associate the discount with specific categories or loyalty programs
            if(request.CategoryIds != null &&  request.CategoryIds.Any())
            {
                newDiscount.ForCategories = _context.ItemCategories
                    .Where(category => request.CategoryIds.Contains(category.Id))
                    .ToList();
            }
            if(request.LoyaltyProgramIds != null && request.CategoryIds.Any())
            {
                newDiscount.ForLoyaltyPrograms = _context.LoyaltyPrograms
                    .Where(loyaltyProgram => request.LoyaltyProgramIds.Contains(loyaltyProgram.Id))
                    .ToList();
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
                .Include(d => d.ForSpecificItem)
                .Include(d => d.ForCategories)
                .Include(d => d.ForLoyaltyPrograms)
                .Select(d => (Discount)d)
                .ToList();

        public Discount GetDiscountById(int discountId) =>
            _context.Discounts
            .Include (d => d.ForSpecificItem)
            .Include(d => d.ForCategories)
            .Include(d => d.ForLoyaltyPrograms)
            .Where(d => d.Id == discountId)
            .Select(d => (Discount)d)
            .First();

        public Discount UpdateDiscount(EditDiscountRequest request)
        {
            var existingDiscount = _context.Discounts
                .Include(d => d.ForSpecificItem)
                .Include(d => d.ForCategories)
                .Include(d => d.ForLoyaltyPrograms)
                .First(d => d.Id == request.Id);

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

            // Update the relationships
            if (request.SpecificItemId > 0)
            {
                existingDiscount.ForSpecificItem = _context.Items.Find(request.SpecificItemId);
            }
            else if(request.SpecificItemId == null)
            {
                existingDiscount.ForSpecificItem = null;
            }
             
            // Add new Category associations
            if(request.CategoryIds != null && request.CategoryIds.Any())
            {
                foreach(var categoryId in request.CategoryIds)
                {
                    var category = _context.ItemCategories.Find(categoryId);
                    if(category != null && !existingDiscount.ForCategories.Any(c => c.Id == categoryId))
                    {
                        existingDiscount.ForCategories.Add(category);
                    }
                }
            }

            // Add new Loyalty Program associations
            if (request.LoyaltyProgramIds != null && request.LoyaltyProgramIds.Any())
            {
                foreach(var loyaltyProgramId in  request.LoyaltyProgramIds)
                {
                    var loyaltyProgram = _context.LoyaltyPrograms.Find(loyaltyProgramId);
                    if(loyaltyProgram != null && !existingDiscount.ForLoyaltyPrograms.Any(l => l.Id == loyaltyProgramId))
                    {
                        existingDiscount.ForLoyaltyPrograms.Add(loyaltyProgram);
                    }
                }
            }

            // Remove existing Category associations if not provided in the request
            if (request.CategoryIds == null || !request.CategoryIds.Any())
            {
                existingDiscount.ForCategories.Clear();
            }
            else
            {
                // Remove missing Category associations
                var categoriesToRemove = existingDiscount.ForCategories
                    .Where(c => !request.CategoryIds.Contains(c.Id))
                    .ToList();

                if (categoriesToRemove.Any())
                {
                    foreach (var categoryToRemove in categoriesToRemove)
                    {
                        existingDiscount.ForCategories.Remove(categoryToRemove);
                    }
                }
            }

            // Remove existing Loyalty Program associations if not provided in the request
            if (request.LoyaltyProgramIds == null || !request.LoyaltyProgramIds.Any())
            {
                existingDiscount.ForLoyaltyPrograms.Clear();
            }
            else
            {
                // Remove missing Loyalty Program associations
                var loyaltyProgramsToRemove = existingDiscount.ForLoyaltyPrograms
                    .Where(l => !request.LoyaltyProgramIds.Contains(l.Id))
                    .ToList();

                if (loyaltyProgramsToRemove.Any())
                {
                    foreach (var loyaltyProgramToRemove in loyaltyProgramsToRemove)
                    {
                        existingDiscount.ForLoyaltyPrograms.Remove(loyaltyProgramToRemove);
                    }
                }
            }

            _context.SaveChanges();

            return (Discount)existingDiscount;
        }
    }
}
