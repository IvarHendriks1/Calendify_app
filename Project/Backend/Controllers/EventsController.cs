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


        [HttpGet]
        public IActionResult getEvents()
        {
            bool Login = true;
            if (HttpContext.Session.GetString("UserLoggedIn") is null) Login = false; //als er niet is ingelogd zet de login bool op false

            if (!Login)
            {
                return Unauthorized("User is not logged in.");
            }

            if (_context.Events.Count() != 0)
            {
                return Ok(_context.Events.ToList());
            }
            return BadRequest("there are no events in the database");

        }

        [HttpGet("{id}")]
        public IActionResult getEvent(int id)
        {
            bool Login = true;
            if (HttpContext.Session.GetString("UserLoggedIn") is null) Login = false; //als er niet is ingelogd zet de login bool op false


            if (!Login)
            {
                return Unauthorized("User is not logged in.");
            }

            if (_context.Events.Count() != 0)
            {

                Event? eventItem = _context.Events.SingleOrDefault(x => x.Id == id);
                if (eventItem == null)
                {
                    return NotFound($"Could not find Event with id {id}");
                }
                return Ok(eventItem);
            }
            return BadRequest("there are no events in the database");
        }

        [HttpPost]
        public IActionResult AddEvent([FromBody] Event eventToAdd)

        {
            // Simulated login state (replace with actual logic once login is ready)
            bool Login = true;

            if (!Login)
            {
                return Unauthorized("Admin is not logged in.");
            }

            if (eventToAdd is not Event)
            {
                return BadRequest($"{eventToAdd} \nis not an event");
            }

            var eventExists = _context.Events.FirstOrDefault(e => e.Id == eventToAdd.Id);
            if (eventExists is null)
            {
                _context.Events.Add(eventToAdd);
                _context.SaveChanges();
                return Ok(new
                {
                    message = $"Event with id {eventToAdd.Id} has been added succesfully",
                    addedEvent = eventToAdd
                });
            }
            else if (eventExists is Event)
            {
                return BadRequest("event already exists");
            }
            return BadRequest("given event could not be added");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            bool Login = true;

            if (!Login)
            {
                return Unauthorized("Admin is not logged in.");
            }
            var eventToDelete = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventToDelete is not null)
            {
                _context.Events.Remove(eventToDelete);
                _context.SaveChanges();
                return Ok(new
                {
                    message = $"Event with id {id} has been deleted successfully",
                    deletedEvent = eventToDelete
                });
            }
            else
            {
                return BadRequest($"event {id} doesn't exists");
            }
        }

        [HttpPut]
        public IActionResult updateEvent([FromBody] Event updatedEvent)
        {
            bool Login = true;

            if (!Login)
            {
                return Unauthorized("Admin is not logged in.");
            }


            var eventToUpdate = _context.Events.FirstOrDefault(e => e.Id == updatedEvent.Id);
            if (eventToUpdate is not null)
            {
                _context.Events.Remove(eventToUpdate);
                _context.Events.Add(updatedEvent);
                _context.SaveChanges();
                return Ok(new
                {
                    message = $"Event with id {updatedEvent.Id} has been updated successfully",
                    updatedVersion = updatedEvent,
                    olderVersion = eventToUpdate
                });
            }
            else
            {
                return BadRequest($"event {updatedEvent.Id} doesn't exists");
            }
        }
    }
}
