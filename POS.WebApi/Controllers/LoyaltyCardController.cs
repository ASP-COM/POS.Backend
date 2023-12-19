using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;
using POS.DB.Models;

namespace POS.WebApi.Controllers
{
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

            // Set related entities to null in the response
            // Setting related(child) entities to null in the response is a common practice to break the serialization cycle,
            // especially when we are not interested in including the complete details of related entities in the response.
            //IMPORTANT However, we can use techniques like lazy loading, eager loading, explicit loading, or JSON serializer
            newLoyaltyCard.User = null;
            newLoyaltyCard.LoyaltyProgram = null;

            return CreatedAtRoute("GetLoyaltyCardById", new {id = newLoyaltyCard.Id}, newLoyaltyCard);
        }

    }

}
