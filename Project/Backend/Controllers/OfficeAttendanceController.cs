using System.Text;
using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Services;
using CalendifyApp.Models;
using CalendifyApp.Filters;

namespace CalendifyApp.Controllers
{
    [AuthorizationFilter]
    [ApiController]
    [Route("[controller]")]
    public class OfficeAttendanceController : ControllerBase
    {
        private readonly IOfficeAttendanceService _attendanceService;

        public OfficeAttendanceController(IOfficeAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> AttendOffice([FromBody] Attendance attendance)
        {
            if (await _attendanceService.IsDateBookedAsync(attendance.UserId, attendance.Date))
            {
                return Conflict("The selected time is already booked. Please choose another time.");
            }

            await _attendanceService.AddAttendanceAsync(attendance);
            return Ok($"Time booked successfully. BookingId = {attendance.Id}");
        }

        [HttpDelete("{userId}/{date}")]
        public async Task<IActionResult> UnAttend(int userId, DateOnly date)
        {
            if (await _attendanceService.RemoveAttendanceAsync(userId, date))
            {
                return Ok("Booking has been cancelled.");
            }

            return NotFound("Attendance record not found for the given user and date.");
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> SeeOffice(DateOnly date)
        {
            var userIds = await _attendanceService.GetUserIdsByDateAsync(date);
            return userIds.Count > 0 ? Ok(userIds) : NotFound("No attendance records found for the given date.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> ConsecutiveDays(int userId)
        {
            var dates = await _attendanceService.GetAttendanceDatesByUserAsync(userId);
            return dates.Count > 0 ? Ok(dates) : NotFound("No attendance records found for the given user.");
        }
    }
}
