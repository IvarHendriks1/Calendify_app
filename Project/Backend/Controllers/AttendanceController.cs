using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CalendifyApp.Models;

namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly MyContext _context;

        public AttendanceController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AttendEvent([FromBody] AttendanceDto attendance)
        {
            // Simulated login state (replace with actual logic once login is ready)
            bool Login = true;

            if (!Login)
            {
                return Unauthorized("User is not logged in.");
            }

            // Check if the event exists
            var eventExists = _context.Events.Any(e => e.Id == attendance.EventId);
            if (!eventExists)
            {
                return NotFound("Event not found.");
            }

            // Add the attendance record
            var newAttendance = new Attendance
            {
                UserId = attendance.UserId,
                EventId = attendance.EventId,
                IsPresent = false // set default value
            };

            _context.Attendance.Add(newAttendance);
            _context.SaveChanges();

            return Ok(newAttendance);
        }
    }
}
