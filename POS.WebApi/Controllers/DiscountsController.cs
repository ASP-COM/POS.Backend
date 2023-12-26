using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;

namespace POS.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public IActionResult GetDiscounts()
        {
            return Ok(_discountService.GetAllDiscounts());
        }

        [HttpGet("{discountId}", Name = "GetDiscountById")]
        public IActionResult GetDiscountById(int discountId)
        {
            return Ok(_discountService.GetDiscountById(discountId));
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountRequest request)
        {
            var newDiscount = _discountService.CreateDiscount(request);
            return CreatedAtRoute("GetDiscountById", new {discountId =  newDiscount.Id}, newDiscount);
        }

        [HttpPut]
        public IActionResult EditDiscount(EditDiscountRequest request)
        {
            return Ok(_discountService.UpdateDiscount(request));
        }

        [HttpDelete("{discountId}")]
        public IActionResult DeleteDiscount(int discountId)
        {
            _discountService.DeleteDiscount(discountId);
            return NoContent();
        }

        
    }
}
