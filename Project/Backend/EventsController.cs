// using Microsoft.AspNetCore.Mvc;
// using CalendifyApp.Models;
// using System.Linq;

// namespace CalendifyApp
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class EventsController : ControllerBase
//     {
//         private readonly MyContext _context;

//         public EventsController(MyContext context)
//         {
//             _context = context;
//         }

//         [HttpGet("get")]
//         public IActionResult getEvent([FromQuery] int id = 0)
//         {
//             if (id == 0)
//             {
//                 return Ok(_context.Events.ToList());
//             }
//             return Ok($"event with id {id}");
//         }

//         [HttpPost]
//         public IActionResult AddEvent([FromBody] Event eventToAdd)
//         {
//             // Simulated login state (replace with actual logic once login is ready)
//             bool Login = true;

//             if (!Login)
//             {
//                 return Unauthorized("User is not logged in.");
//             }

//             // Check if the event exists
//             var eventExists = _context.Events.Any(e => e.EventId == eventToAdd.EventId);
//             if (!eventExists)
//             {
//                 _context.Events.Add(eventToAdd);
//                 _context.SaveChanges();
//                 return Ok($"{eventToAdd} has been added succesfully");
//             }
//             else if (eventExists)
//             {
//                 return BadRequest("event already exists");
//             }
//             return BadRequest("given event could not be added");
//         }
//     }
// }
