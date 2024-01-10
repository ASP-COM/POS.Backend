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
                    return Ok(newOrder);
                }
                return BadRequest("Invalid request");

            } catch(Exception ex) 
            {
                return BadRequest("An error occured" + ex.Message);
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
                    return Ok(updatedOrder);
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
            var updatedOrder = _orderService.RemoveItems(id, itemsIds);
            if (updatedOrder != null)
            {
                return Ok(updatedOrder);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}/apply_voucher", Name = "ApplyVoucher")]
        public IActionResult ApplyVoucher(int id, int voucherId) 
        {
            var updatedOrder = _orderService.ApplyVoucher(id, voucherId);
            if (updatedOrder != null) { 
                return Ok(updatedOrder);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}/pay", Name= "PayForOrder")]
        public IActionResult PayForOrder(int id, string type) 
        {
            var updatedOrder = _orderService.PayForOrder(id, type.ToLower());
            if (updatedOrder != null)
            {
                return Ok(updatedOrder);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}/add_tip", Name = "AddTip")]
        public IActionResult AddTip(int id, decimal tip) // Apply tip
        {
            var updatedOrder = _orderService.AddTip(id, tip);
            if (updatedOrder != null)
            {
                return Ok(updatedOrder);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/invoice")]
        public IActionResult GenerateInvoice(int id)
        {
            var invoice = _orderService.GetOrderInvoice(id);
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
