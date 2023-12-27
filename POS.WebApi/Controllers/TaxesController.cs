using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;

namespace POS.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaxesController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxesController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet]
        public IActionResult GetAllTaxes()
        {
            return Ok(_taxService.GetAllTaxes());
        }

        [HttpGet("{id}", Name = "GetTaxById")]
        public IActionResult GetTaxById(int id)
        {
            return Ok(_taxService.GetTaxById(id));
        }

        [HttpPost]
        public IActionResult CreateTax(CreateTaxRequest request)
        {
            var newTax = _taxService.CreateTax(request);
            return CreatedAtRoute("GetTaxById", new {id =  newTax.Id}, newTax);
        }

        [HttpPut]
        public IActionResult UpdateTax(UpdateTaxRequest request)
        {
            return Ok(_taxService.UpdateTax(request));
        }

        [HttpDelete]
        public IActionResult DeleteTaxById(int id)
        {
            _taxService.DeleteTaxById(id);
            return NoContent();
        }
    }
}
