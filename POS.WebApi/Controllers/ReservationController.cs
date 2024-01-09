using POS.Core;
using POS.Core.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using POS.DB.Models;
using POS.Core.DTO;
using POS.Core.Services;

namespace POS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;

        public ReservationController(IUserService userService, IReservationService reservationService)
        {
            _userService = userService;
            _reservationService = reservationService;
        }

        [HttpPost("create_reservation_slot")]
        public IActionResult CreateReservationSlot(ReservationCreationRequest request)
        {
            try
            {
                var result = _reservationService.CreateReservationSlot(request);
                if (result == null)
                {
                    return StatusCode(400, "Invalid request");
                }
                else
                {
                    // FIXME: Don't return ACTUAL object, because we expose employee password
                    return Created("", result);
                }
            }
            catch
            {
                return StatusCode(400, "An error occured");
            }
        }

        [HttpDelete("remove_reservation")]
        public IActionResult RemoveReservation(int id)
        {
            // FIXME: Validate that action is being performed by employee
            try {
                var result = _reservationService.RemoveServation(id);
                if (result) {
                    return StatusCode(200, "Removed reservation");
                } else {
                    return StatusCode(400, "Invalid request");
                }
            } catch {
                return StatusCode(400, "An error occured");
            }
        }


        [HttpPatch("claim_reservation")]
        public IActionResult ClaimReservation(ReservationClaimRequest request)
        {
            // FIXME: userId should be gotten from token
            try
            {
                var result = _reservationService.ClaimReservation(request.reservationId, request.userId);
                if (result)
                {
                    return StatusCode(200, "Claimed reservation");
                }
                else
                {
                    return StatusCode(400, "Invalid request");
                }
            }
            catch
            {
                return StatusCode(400, "An error occured");
            }
        }

        [HttpPatch("release_reservation")]
        public IActionResult ReleaseReservation(int reservationId)
        {
            // FIXME: Validate that user is the same one who claimed the reservation
            // FIXME: Employees should also be able to release reservations
            try
            {
                var result = _reservationService.ReleaseReservation(reservationId);
                if (result)
                {
                    return StatusCode(200, "Released reservation");
                }
                else
                {
                    return StatusCode(400, "Invalid request");
                }
            }
            catch
            {
                return StatusCode(400, "An error occured");
            }
        }

        [HttpGet("get_user_reservations")]
        public IActionResult GetUserReservations(int userId)
        {
            try {
                var result = _reservationService.GetUserReservations(userId);
                return Ok(result);
            } catch {
                return StatusCode(400, "An error occured");
            }
        }

        [HttpGet("get_free_reservations")]
        public IActionResult GetFreeReservations(FreeReservationsRequest request)
        {
            try {
                var result = _reservationService.GetFreeReservationsStartingOnDate(request.EmployeeId, request.ServiceId, request.Start);
                return Ok(result);
            } catch {
                return StatusCode(400, "An error occured");
            }
        }

        [HttpGet("get_free_days")]
        public IActionResult GetFreeDays(GetAvailableDaysRequest request)
        {
            var result = _reservationService.GetDatesWithFreeReservationsInRange(request.BusinessId, request.EmployeeId, request.ServiceId, request.Start, request.End);
            return Ok(result);
        }
    }
}
