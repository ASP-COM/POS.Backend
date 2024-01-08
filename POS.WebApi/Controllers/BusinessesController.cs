using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;
using POS.DB.Models;

namespace POS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public IActionResult GetBusinesses()
        {
            return Ok(_businessService.GetBusinesses());
        }

        [HttpGet("{id}", Name = "GetBusinessById")]
        public IActionResult GetBusinessById(int id)
        {
            return Ok(_businessService.GetBusinessById(id));
        }

        // Added name segment the way routing system can distinguish between the two actions
        [HttpGet("name{name}", Name = "GetBusinessByName")]
        public IActionResult GetBusinessByName(string name)
        {
            return Ok(_businessService.GetBusinessByName(name));
        }

        [HttpPost]
        public IActionResult CreateBusiness(CreateBusinessRequest business)
        {
            var newBusiness = _businessService.CreateBusiness(business.BusinessName);
            return CreatedAtRoute("GetBusinessById", new { id = newBusiness.Id }, newBusiness);
        }

        [HttpGet("{id}/employees", Name = "GetBusinessEmployees")]
        public IActionResult GetBusinessEmployees(int id)
        {
            var employees = _businessService.GetBusinessEmployees(id);
            return employees == null ? NotFound() : Ok(employees);
        }
    }
}
