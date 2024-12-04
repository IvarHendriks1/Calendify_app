using CalendifyApp.Filters;
using CalendifyApp.Models;
using CalendifyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizationFilter]
    public class EventAttendanceController : ControllerBase
    {
        private readonly IEventAttendanceService _service;

        public EventAttendanceController(IEventAttendanceService service)
        {
            _service = service;
        }

        [HttpPost("attend")]
        public IActionResult AttendEvent([FromBody] AttendanceDto attendance)
        {
            try
            {
                var success = _service.AttendEvent(attendance);
                if (!success)
                    return NotFound("User or event not found.");

                return Ok("Registration successfully recorded.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("attendees/{eventId}")]
        public IActionResult GetEventAttendees(int eventId)
        {
            var attendees = _service.GetEventAttendees(eventId);

            if (attendees == null || !attendees.Any())
                return NotFound("No attendees found for this event.");

            return Ok(attendees);
        }

        [HttpGet("user/{userId}/attended-events")]
        public IActionResult GetEventsByUser(int userId)
        {
            var events = _service.GetEventsByUser(userId);

            if (events == null || !events.Any())
                return NotFound("No attended events found for this user.");

            return Ok(events);
        }

        [HttpDelete("remove")]
        public IActionResult RemoveAttendance([FromBody] AttendanceDto attendance)
        {
            var success = _service.RemoveAttendance(attendance);

            if (!success)
                return NotFound("Attendance or event not found.");

            return Ok("Registration successfully removed.");
        }
    }
}
