using CalendifyApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventAttendanceController : ControllerBase
    {
        private readonly MyContext _context;

        // Constructor to inject the database context
        public EventAttendanceController(MyContext context)
        {
            _context = context;
        }

        // Method to check if user is logged in
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserLoggedIn") != null;
        }

        // POST: User attends an event
        [HttpPost("attend")]
        public IActionResult AttendEvent([FromBody] AttendanceDto attendance)
        {
            // Check if user is logged in
            if (!IsUserLoggedIn())
            {
                return Unauthorized("User is not logged in.");
            }

            // Find the user and event in the database
            var user = _context.Users.FirstOrDefault(u => u.Id == attendance.UserId);
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);

            // Check if the user or event does not exist
            if (user == null || eventEntity == null)
            {
                return NotFound("User or event not found.");
            }

            // Check if the event has already started or ended
            if (eventEntity.Date.ToDateTime(eventEntity.StartTime) < DateTime.Now)
            {
                return BadRequest("The event has already started or ended.");
            }

            // Check if the user is already registered for this event
            var existingAttendance = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            if (existingAttendance != null)
            {
                return BadRequest("You are already registered for this event.");
            }

            // Add new attendance
            var newAttendance = new Attendance
            {
                UserId = attendance.UserId,
                Date = eventEntity.Date // Set the event date as the attendance date
            };

            // Save the new attendance in the database
            _context.Attendance.Add(newAttendance);
            _context.SaveChanges();

            return Ok("Registration successfully recorded.");
        }

        // GET: Retrieve list of attendees for an event
        [HttpGet("attendees/{eventId}")]
        public IActionResult GetEventAttendees(int eventId)
        {
            // Check if user is logged in
            if (!IsUserLoggedIn())
            {
                return Unauthorized("User is not logged in.");
            }

            // Retrieve the event date
            var eventDate = _context.Events.FirstOrDefault(e => e.Id == eventId)?.Date;

            // Check if the event exists
            if (eventDate == null)
            {
                return NotFound("Event not found.");
            }

            // Retrieve list of attendees for the specified date
            var eventAttendees = _context.Attendance
                .Where(a => a.Date == eventDate) // Filter by event date
                .Join(
                    _context.Users, // Join with Users table
                    attendance => attendance.UserId, // Match on UserId
                    user => user.Id, // Match on Id from Users
                    (attendance, user) => new
                    {
                        attendance.UserId, // UserId of the attendee
                        UserName = user.First_name + " " + user.Last_name // Full name of the user
                    }
                )
                .ToList(); // Convert to list

            // Check if no attendees were found
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
            // Check if user is logged in
            if (!IsUserLoggedIn())
            {
                return Unauthorized("User is not logged in.");
            }

            // Retrieve the event to get the date
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);
            if (eventEntity == null)
            {
                return NotFound("Event not found.");
            }

            // Find the user's attendance record for the specified event
            var attendanceRecord = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            // Check if the attendance record exists
            if (attendanceRecord == null)
            {
                return NotFound("Attendance not found.");
            }

            // Remove the attendance and save changes
            _context.Attendance.Remove(attendanceRecord);
            _context.SaveChanges();

            return Ok("Registration successfully removed.");
        }
    }
}
