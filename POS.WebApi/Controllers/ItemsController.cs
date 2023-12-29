using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;

namespace POS.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("all")]
        public IActionResult GetAllItems()
        {
            return Ok(_itemService.GetAllItems());
        }

        [HttpDelete] public IActionResult DeleteItemById(int id)
        {
            _itemService.DeleteItemById(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateItem(CreateItemRequest request)
        {
            var newItem = _itemService.CreateItem(request);
            return CreatedAtRoute("GetItemById", new {id = newItem.Id}, newItem);
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public IActionResult GetItemById(int id)
        {
            return Ok(_itemService.GetItemById(id));
        }

        [HttpGet("services")]
        public IActionResult GetAllItemsServices()
        {
            return Ok(_itemService.GetAllItemsServices());
        }

        [HttpGet("business/{businessId}", Name = "GetAllItemsByBusinessId")]
        public IActionResult GetAllItemsByBusinessId(int businessId)
        {
            return Ok(_itemService.GetAllItemsByBusinessId(businessId));
        }

        [HttpPut]
        public IActionResult UpdateItem(EditItemRequest request)
        {
            return Ok(_itemService.UpdateItem(request));
        }



    }
}
