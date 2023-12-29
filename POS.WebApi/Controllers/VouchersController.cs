using POS.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using POS.Core;

namespace POS.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VouchersController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VouchersController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        /*
         Voucher CreateVoucher(CreateVoucherRequest request);
        Voucher EditVoucher(EditVoucherRequest request);
        void DeleteVoucherById(int voucherId);

        Voucher GetVoucherById(int id);
        List<Voucher> GetAllVouchers();
        List<Voucher> GetVouchersByBusinessId(int businessId);
         */

        [HttpGet]
        public IActionResult GetAllVouchers()
        {
            return Ok(_voucherService.GetAllVouchers());
        }

        [HttpGet("business/{businessId}", Name = "GetVouchersByBusinessId")]
        public IActionResult GetVouchersByBusinessId(int businessId)
        {
            return Ok(_voucherService.GetVouchersByBusinessId(businessId));
        }

        [HttpGet("{id}", Name = "GetVoucherById")]
        public IActionResult GetVoucherById(int id)
        {
            return Ok(_voucherService.GetVoucherById(id));
        }

        [HttpDelete]
        public IActionResult DeleteVoucher(int id)
        {
            _voucherService.DeleteVoucherById(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateVoucher(CreateVoucherRequest request)
        {
            var newVoucher = _voucherService.CreateVoucher(request);
            return CreatedAtRoute("GetVoucherById", new { id = newVoucher.Id }, newVoucher);
        }

        [HttpPut]
        public IActionResult EditVoucher(EditVoucherRequest request)
        {
            return Ok(_voucherService.EditVoucher(request));
        }
    }
}
