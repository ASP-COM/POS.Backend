using Microsoft.AspNetCore.Mvc;
using POS.Core.DTO;
using POS.Core.Services;

// TODO
// Fix permisions everywhere.
// Add more descriptive errors.

namespace POS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateNewOrder(CreateOrderRequest createOrderRequest)
        {
            try
            {
                var newOrder = _orderService.CreateNewOrder(createOrderRequest);
                if(newOrder != null){
                    return CreatedAtRoute(new { id = newOrder.Id }, newOrder);
                }
                return BadRequest("Invalid request");

            } catch
            {
                return BadRequest("An error occured");
            }
        }

        [HttpDelete("{id}/cancel", Name = "CancelOrder")]
        public IActionResult CancelOrder(int id) {
            if (_orderService.CancelOrder(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // If item already exists patches the existing orderline if not creates a new one
        [HttpPatch("{id}/add_items", Name = "AddItems")]
        public IActionResult AddItems(int id, List<CreateOrderLineRequest> orderLineRequest)
        {
            try
            {
                var updatedOrder = _orderService.AddAdditionalItems(id, orderLineRequest);
                if (updatedOrder != null)
                {
                    return CreatedAtRoute(new { id = updatedOrder.Id }, updatedOrder);
                }
                return BadRequest("Invalid request");
            }
            catch
            {
                return BadRequest("An error occured");
            }
        }

        [HttpPatch("{id}/remove_items", Name = "RemoveItems")]
        public IActionResult RemoveItems(int id, List<int> itemsIds) 
        {
            if (_orderService.RemoveItems(id, itemsIds))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}/apply_voucher", Name = "ApplyVoucher")]
        public IActionResult ApplyVoucher(int id, int voucherId) 
        {
            if(_orderService.ApplyVoucher(id, voucherId)) { 
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // TODO: If card exists apply discounts and assign holder of card to the report as customer
        [HttpPatch("{id}/apply_discount_card")]
        public IActionResult ApplyDiscount(int dicountCardId)
        {
            return Ok();
        }

        // TODO: mark order as payed
        // Type should be provided here can be only: Card, cash or none if whole price was coverted by vouchers.
        // But additional checks for voucher use should be made to set the final status.
        [HttpPatch("{id}/pay")]
        public IActionResult PayForOrder() 
        {
            return Ok();
        }

       // TODO: update the tip
       [HttpPatch("{id}/add_tip")]
        public IActionResult AddTip(decimal tip) // Apply tip
        {
            return Ok();
        }

        // TODO: calculate and return receipt (invoice) - a big and important one 
        [HttpGet("{id}/invoice")]
        public IActionResult GenerateInvoice() // Create new DTO for calculations
        {
            return Ok();
        }


    }
}
