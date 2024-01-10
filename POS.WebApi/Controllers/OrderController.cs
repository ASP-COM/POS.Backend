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

        [HttpPatch("{id}/pay", Name= "PayForOrder")]
        public IActionResult PayForOrder(int id, string type) 
        {
            if(_orderService.PayForOrder(id, type))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}/add_tip", Name = "AddTip")]
        public IActionResult AddTip(int id, decimal tip) // Apply tip
        {
            if (_orderService.AddTip(id, tip))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/invoice")]
        public IActionResult GenerateInvoice(int orderId) // Create new DTO for calculations
        {
            var invoice = _orderService.GetOrderInvoice(orderId);
            if (invoice != null)
            {
                return Ok(invoice);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
