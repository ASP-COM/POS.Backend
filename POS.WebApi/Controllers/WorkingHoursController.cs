using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.DTO;


namespace POS.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WorkingHoursController : ControllerBase
    {
        private readonly  IWorkingHoursService _workingHoursService;

        public WorkingHoursController(IWorkingHoursService workingHoursService)
        {
            _workingHoursService = workingHoursService;
        }

        /*
          WorkingHours CreateWorkingHours(CreateWorkingHoursRequest request);

        WorkingHours GetWorkingHoursById(int id); DONE
        List <WorkingHours> GetAllWorkingHours(); DONE
        List <WorkingHours> GetWorkingHoursByBusinessId(int businessId); DONE
        List <WorkingHours> GetWorkingHoursByEmployeeId(int userId); DONE

        void DeleteWorkingHoursById(int id);
         */

        [HttpGet]
        public IActionResult GetAllWorkingHours()
        {
            return Ok(_workingHoursService.GetAllWorkingHours());
        }

        [HttpGet("business/{businessId}", Name = "GetWorkingHoursByBusinessId")]
        public IActionResult GetWorkingHoursByBusinessId(int businessId)
        {
            return Ok(_workingHoursService.GetWorkingHoursByBusinessId(businessId));
        }

        [HttpGet("employee/{employeeId}", Name = "GetWorkingHoursByEmployeeId")]
        public IActionResult GetWorkingHoursByEmployeeId(int employeeId)
        {
            return Ok(_workingHoursService.GetWorkingHoursByEmployeeId(employeeId));
        }

        [HttpGet("{id}", Name = "GetWorkingHoursById")]
        public IActionResult GetWorkingHoursById(int id)
        {
            return Ok(_workingHoursService.GetWorkingHoursById(id));
        }

        [HttpDelete]
        public IActionResult DeleteWorkingHoursByid(int id)
        {
            _workingHoursService.DeleteWorkingHoursById(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateWorkingHours(CreateWorkingHoursRequest request)
        {
            var newWorkingHours = _workingHoursService.CreateWorkingHours(request);
            return CreatedAtRoute("GetWorkingHoursById", new { id = newWorkingHours.Id }, newWorkingHours);
        }

    }
}
