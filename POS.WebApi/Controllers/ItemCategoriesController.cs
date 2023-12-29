using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;

namespace POS.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemCategoriesController : ControllerBase
    {
        private readonly IItemCategoryService _itemCategoryService;

        public ItemCategoriesController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }

        [HttpGet]
        public IActionResult GetAllItemCategories()
        {
            return Ok(_itemCategoryService.GetItemCategories());
        }

        [HttpGet("{itemCategoryId}", Name= "GetItemCategoryById")]
        public IActionResult GetItemCategoryById(int itemCategoryId)
        {
            return Ok(_itemCategoryService.GetItemCategoryById(itemCategoryId));
        }

        [HttpGet("business/{businessId}", Name = "GetItemCategoryByBusinessId")]
        public IActionResult GetItemCategoriesByBusinessId(int businessId)
        {
            return Ok(_itemCategoryService.GetItemCategoriesByBusinessId(businessId));
        }

        [HttpPost]
        public IActionResult CreateItemCategory(CreateItemCategoryRequest request)
        {
            var newItemCategory = _itemCategoryService.CreateItemCategory(request);
            return CreatedAtRoute("GetItemCategoryById", new {itemCategoryId =  newItemCategory.Id}, newItemCategory);
        }

    }
}
