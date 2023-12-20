using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;
using POS.DB.Models;

namespace POS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoyaltyCardsController : ControllerBase
    {
        private readonly ILoyaltyCardService _loyaltyCardService;

        public LoyaltyCardsController(ILoyaltyCardService loyaltyCardService)
        {
            _loyaltyCardService = loyaltyCardService;
        }

        [HttpGet]
        public IActionResult GetLoyaltyCards()
        {
            return Ok(_loyaltyCardService.GetLoyaltyCards());
        }

        [HttpGet("{id}", Name = "GetLoyaltyCardById")]
        public IActionResult GetLoyaltyCardById(int id)
        {
            return Ok(_loyaltyCardService.GetLoyaltyCardById(id));
        }

        [HttpGet("user/{userId}", Name = "GetLoyaltyCardByUserId")]
        public IActionResult GetLoyaltyCardsById(int userId)
        {
            return Ok(_loyaltyCardService.GetLoyaltyCardsByUserId(userId));
        }

        [HttpGet("loyalty_program/{loyaltyProgramId}", Name = "GetLoyaltyCardsByLoyaltyProgramId")]
        public IActionResult GetLoyaltyCardsByLoyaltyProgramId(int loyaltyProgramId)
        {
            return Ok(_loyaltyCardService.GetLoyaltyCardsByLoyaltyProgramId(loyaltyProgramId));
        }

        [HttpGet("card_code/{cardCode}", Name = "GetLoyaltyCardByCardCode")]
        public IActionResult GetLoyaltyCardByCardCode(string cardCode)
        {
            return Ok(_loyaltyCardService.GetLoyaltyCardByCardCode(cardCode));
        }

        [HttpPost]
        public IActionResult CreateLoyaltyCard(CreateLoyaltyCardRequest request)
        {
            var newLoyaltyCard = _loyaltyCardService.CreateLoyaltyCard(request);
            return CreatedAtRoute("GetLoyaltyCardById", new {id = newLoyaltyCard.Id}, newLoyaltyCard);
        }

    }

}
