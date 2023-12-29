using POS.DB;
using POS.Core.DTO;

namespace POS.Core
{
    public class TaxService : ITaxService
    {
        private readonly AppDbContext _context;

        public TaxService(AppDbContext context)
        {
            _context = context;
        }



        public void DeleteTaxById(int id)
        {
            var tax = _context.Tax.Find(id);

            if(tax !=  null)
            {
                _context.Tax.Remove(tax);
                _context.SaveChanges();
            }
        }

        public List<Tax> GetAllTaxes() =>
            _context.Tax
                .Select(t => (Tax)t)
                .ToList();

        public Tax GetTaxById(int id) =>
            _context.Tax
                .Where(t => t.Id == id)
                .Select(t => (Tax)t)  
                .First();

        public Tax UpdateTax(UpdateTaxRequest request)
        {
            var existingTax = _context.Tax.Find(request.Id);

            if(existingTax != null)
            {
                // Map Properties from the request to update the Tax entity
                existingTax.Description = request.Description;
                existingTax.AmountPct = request.AmountPct;
            }

            _context.SaveChanges();

            return (Tax)existingTax;
        }

        public Tax CreateTax(CreateTaxRequest request)
        {
            var newTax = new DB.Models.Tax
            {
                Description = request.Description,
                AmountPct = request.AmountPct,
            };

            _context.Tax.Add(newTax);
            _context.SaveChanges();

            return (Tax)newTax;
        }
    }
}
