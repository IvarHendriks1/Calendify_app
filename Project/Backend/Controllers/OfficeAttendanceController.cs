using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CalendifyApp.Models;

namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfficeAttendanceController : ControllerBase
    {
        private readonly MyContext _context;

        public OfficeAttendanceController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AttendOffice([FromBody] Attendance attendance)
        {
            // Validate the attendance object
            if (attendance == null || attendance.UserId <= 0)
            {
                return BadRequest(new { message = "Invalid attendance data." });
            }

            // Check if the user has already booked the selected date
            bool attendanceExists = await _context.Attendance
                .AnyAsync(a => a.UserId == attendance.UserId && a.Date == attendance.Date);

            if (attendanceExists)
            {
                return Conflict(new { message = "The selected time is already booked. Please choose another time." });
            }

            // Add new booking for the date
            await _context.Attendance.AddAsync(attendance);
            await _context.SaveChangesAsync();

            // Return a created response with the ID of the newly created booking
            return CreatedAtAction(nameof(AttendOffice), new { id = attendance.Id }, new { message = "Time booked successfully.", bookingId = attendance.Id });
        }

        [HttpDelete("{userId}/{date}")]
        public IActionResult UnAttend(int userId, DateOnly date) // Using DateOnly to match your model
        {
            // Find the existing attendance record for the user on the specified date
            var existingAttendance = _context.Attendance
                .FirstOrDefault(a => a.UserId == userId && a.Date == date);

            if (existingAttendance != null)
            {
                _context.Attendance.Remove(existingAttendance); // Remove the record
                _context.SaveChanges(); // Save changes to the database
                return Ok(new { message = "Booking has been cancelled"}); // Return 204 No Content
            }

            return NotFound(new { message = "Attendance record not found for the given user and date." });
        }
    }
}
