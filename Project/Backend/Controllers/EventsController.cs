using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Models;
using System.Linq;


namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/Events")]
    //[controller]
    public class EventsController : Controller
    {
        private readonly MyContext _context;

        public EventsController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult getEvent(int id)
        {
            if (id == 0)
            {
                return Ok("return all events");
            }
            return Ok($"event with id {id}");

        }

        [HttpPost]
        public IActionResult AddEvent([FromBody] object objEventToAdd)
        {
            // Simulated login state (replace with actual logic once login is ready)
            bool Login = true;

            if (!Login)
            {
                return Unauthorized("User is not logged in.");
            }

            if (objEventToAdd is not Event)
            {
                return BadRequest($"{objEventToAdd} \nis not an event");
            }
            Event eventToAdd = (Event)objEventToAdd;
            // Check if the event exists
            var eventExists = _context.Events.FirstOrDefault(e => e.EventId == eventToAdd.EventId);
            if (eventExists is null)
            {
                _context.Events.Add(eventToAdd);
                _context.SaveChanges();
                return Ok($"{objEventToAdd} has been added succesfully");
            }
            else if (eventExists is Event)
            {
                return BadRequest("event already exists");
            }
            return BadRequest("given event could not be added");
        }
    }
}
