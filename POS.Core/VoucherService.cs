using Microsoft.EntityFrameworkCore;
using POS.Core.DTO;
using POS.DB;

namespace POS.Core
{
    public class VoucherService : IVoucherService
    {
        private readonly AppDbContext _context;

        public VoucherService(AppDbContext context)
        {
            _context = context;
        }

        public Voucher CreateVoucher(CreateVoucherRequest request)
        {
            if(request.ValidFrom > request.ValidTo)
            {
                // Handle the case where invalid date was entered
                // We can throw an exception or handle it based on our application's logic
                // Also, we may create custom exceptions to handle these events
                throw new InvalidOperationException("Inappropriate date entered error");
            }

            var newVoucher = new DB.Models.Voucher
            {
                Description = request.Description,
                ValidFrom = request.ValidFrom,
                ValidTo = request.ValidTo,
                IsUsed = false,
                Amount = request.Amount,
            };

            // Fetch related entities based on relationships
            if(request.BusinessId > 0)
            {
                newVoucher.Business = _context.Businesss.Find(request.BusinessId);
                if(newVoucher.Business != null)
                {
                    newVoucher.BusinessId = request.BusinessId;
                }
            }

            _context.Voucher.Add(newVoucher);
            _context.SaveChanges();

            return (Voucher)newVoucher;
        }

        public void DeleteVoucherById(int voucherId)
        {
            var voucher = _context.Voucher.Find(voucherId);

            if (voucher != null)
            {
                _context.Voucher.Remove(voucher);
                _context.SaveChanges();
            }
        }

        public Voucher EditVoucher(EditVoucherRequest request)
        {
            var existingVoucher = _context.Voucher
                .Include(v => v.Business)
                .Include(v => v.Order)
                .First(v => v.Id == request.Id);

            if (existingVoucher != null)
            {
                existingVoucher.Description = request.Description;
                existingVoucher.ValidFrom = request.ValidFrom;
                existingVoucher.ValidTo = request.ValidTo;
                existingVoucher.IsUsed = request.IsUsed;
                existingVoucher.Amount = request.Amount;

                // Update the relationships
                if(request.BusinessId > 0)
                {
                    existingVoucher.Business = _context.Businesss.Find(request.BusinessId);
                    if(existingVoucher.Business != null)
                    {
                        existingVoucher.Business.Id = request.BusinessId;
                    }
                }

                if(request.OrderId > 0)
                {
                    existingVoucher.Order = _context.Orders.Find(request.OrderId);
                    if(existingVoucher.Order != null)
                    {
                        existingVoucher.OrderId = request.OrderId;
                    }
                }

                _context.SaveChanges();
            }

            return (Voucher)existingVoucher;
        }

        public List<Voucher> GetAllVouchers() =>
            _context.Voucher
                .Include(v => v.Business)
                //.Include(v => v.Order)
                .Select(v => (Voucher)v)
                .ToList();

        public Voucher GetVoucherById(int id) =>
            _context.Voucher
                .Include(v => v.Business)
                //.Include(v => v.Order)
                .Where(v => v.Id == id)
                .Select(v => (Voucher)v)
                .First();

        public List<Voucher> GetVouchersByBusinessId(int businessId) =>
            _context.Voucher
                .Include(v => v.Business)
                //.Include(v => v.Order)
                .Where(v => v.BusinessId == businessId)
                .Select(v => (Voucher)v)
                .ToList();
    }
}
