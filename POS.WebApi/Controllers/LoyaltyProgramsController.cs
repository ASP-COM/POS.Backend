using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.DB.Models;

namespace POS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoyaltyProgramsController : ControllerBase
    {
        private readonly ILoyaltyProgramService _loyaltyProgramService;

        public LoyaltyProgramsController(ILoyaltyProgramService loyaltyProgramService)
        {
            _loyaltyProgramService = loyaltyProgramService;
        }

        [HttpGet]
        public IActionResult GetLoyaltyPrograms()
        {
            return Ok(_loyaltyProgramService.GetLoyaltyPrograms());
        }

        [HttpGet("{id}", Name="GetLoyaltyProgram")]
        public IActionResult GetLoyaltyProgram(int id)
        {
            return Ok(_loyaltyProgramService.GetLoyaltyProgramById(id));
        }

        [HttpPost]
        public IActionResult CreateLoyaltyProgram(LoyaltyProgram loyaltyProgram)
        {
            var newLoyaltyProgram = _loyaltyProgramService.CreateLoyaltyProgram(loyaltyProgram);
            return CreatedAtRoute("GetLoyaltyProgram", new {id = newLoyaltyProgram.Id}, newLoyaltyProgram);
        }
    }
}
