using CalendifyApp.Models;
using CalendifyApp.Filters; // Import the filter
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizationFilter] // Apply filter to the entire controller
    public class EventAttendanceController : ControllerBase
    {
        private readonly MyContext _context;

        public EventAttendanceController(MyContext context)
        {
            _context = context;
        }

        // POST: User attends an event
        [HttpPost("attend")]
        public IActionResult AttendEvent([FromBody] AttendanceDto attendance)
        {
            // Zoek de gebruiker op in de database met het opgegeven UserId uit de attendance
            var user = _context.Users.FirstOrDefault(u => u.Id == attendance.UserId);

            // Zoek het evenement op in de database met het opgegeven EventId uit de attendance
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);

            if (user == null || eventEntity == null)
            {
                return NotFound("User or event not found.");
            }

            if (eventEntity.Date.Add(eventEntity.StartTime) < DateTime.Now)
            {
                return BadRequest("The event has already started or ended.");
            }

            var existingAttendance = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            if (existingAttendance != null)
            {
                return BadRequest("You are already registered for this event.");
            }

            var newAttendance = new Attendance
            {
                UserId = attendance.UserId,
                Date = eventEntity.Date
            };

            _context.Attendance.Add(newAttendance);
            _context.SaveChanges();

            return Ok("Registration successfully recorded.");
        }

        // GET: Retrieve list of attendees for an event
        [HttpGet("attendees/{eventId}")]
        public IActionResult GetEventAttendees(int eventId)
        {
            var eventDate = _context.Events.FirstOrDefault(e => e.Id == eventId)?.Date;

            if (eventDate == null)
            {
                return NotFound("Event not found.");
            }

            var eventAttendees = _context.Attendance
                .Where(a => a.Date == eventDate)
                .Join(
                    _context.Users,
                    attendance => attendance.UserId,
                    user => user.Id,
                    (attendance, user) => new
                    {
                        attendance.UserId,
                        UserName = user.First_name + " " + user.Last_name
                    }
                )
                .ToList();

            if (!eventAttendees.Any())
            {
                return NotFound("No attendees found for this event.");
            }

            return Ok(eventAttendees);
        }

        // GET: Retrieve list of events a user has attended
        [HttpGet("user/{userId}/attended-events")]
        public IActionResult GetEventsByUser(int userId)
        {
            // Check if the userId is valid (positive integer)
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID. Please provide a positive integer value.");
            }

            try
            {
                // Check if the user exists
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Get all events attended by the user
                var attendedEvents = _context.Attendance
                    .Where(a => a.UserId == userId)
                    .Join(
                        _context.Events,
                        attendance => attendance.Date,
                        eventEntity => eventEntity.Date,
                        (attendance, eventEntity) => new
                        {
                            eventEntity.Id,
                            eventEntity.Title,
                            eventEntity.Date,
                            eventEntity.StartTime,
                            eventEntity.EndTime
                        }
                    )
                    .ToList();

                if (!attendedEvents.Any())
                {
                    return NotFound("No attended events found for this user.");
                }

                return Ok(attendedEvents);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while retrieving attended events.");

                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        // DELETE: Remove registration for an event
        [HttpDelete("remove")]
        public IActionResult RemoveAttendance([FromBody] AttendanceDto attendance)
        {
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);
            if (eventEntity == null)
            {
                return NotFound("Event not found.");
            }

            var attendanceRecord = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            if (attendanceRecord == null)
            {
                return NotFound("Attendance not found.");
            }

            _context.Attendance.Remove(attendanceRecord);
            _context.SaveChanges();

            return Ok("Registration successfully removed.");
        }
    }
}
