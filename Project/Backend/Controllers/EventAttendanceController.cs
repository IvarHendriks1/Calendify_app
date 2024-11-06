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

            if (eventEntity.Date.ToDateTime(eventEntity.StartTime) < DateTime.Now)
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
        [AdminFilter] // Apply filter so only admins can access this endpoint
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
