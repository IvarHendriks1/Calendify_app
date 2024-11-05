// using Microsoft.AspNetCore.Mvc;
// using System.Linq;
// using CalendifyApp.Models;

// namespace CalendifyApp.Controllerss
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class OfficeAttendanceController : ControllerBase
//     {

//         [HttpPost]
//         public IActionResult AttendOffice([FromBody] Attendance attendance)
//         {
//             // Simulated login state (replace with actual logic once login is ready)
//             bool Login = true;

//             // if (!Login)
//             // {
//             //     return Unauthorized("User is not logged in.");
//             // }

//             if(Login == true)
//             {
//                 // Check if the event exists
//                 var officeExists = _context.Events.Any(e => e.EventId == attendance.EventId);
//                 if (!officeExists)
//                 {
//                     return NotFound("Event not found.");
//                 }

//                 // Add the attendance record
//                 var newAttendance = new Attendance
//                 {
//                     UserId = attendance.UserId,
//                     Date = attendance.Date
//                 };

//                 _//context.Attendance.Add(newAttendance);
//                 //_context.SaveChanges();

//                 //return Ok(newAttendance);
//             }
//             return Unauthorized("User is not logged in.");
            

//             // Check if the event exists
//             // var officeExists = _context.Events.Any(e => e.EventId == attendance.EventId);
//             // if (!officeExists)
//             // {
//             //     return NotFound("Event not found.");
//             // }

//             // // Add the attendance record
//             // var newAttendance = new Attendance
//             // {
//             //     UserId = attendance.UserId,
//             //     EventId = attendance.EventId,
//             //     IsPresent = false // set default value
//             // };

//             // _context.Attendance.Add(newAttendance);
//             // _context.SaveChanges();

//             // return Ok(newAttendance);
//         }
//     }
// }
