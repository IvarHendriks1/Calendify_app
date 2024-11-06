using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Models;
using System.Linq;
using CalendifyApp.Filters;
using System.Data.Entity.Core.Objects;


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

        [AuthorizationFilter]
        [HttpGet]
        public IActionResult getEvents()
        {
            if (_context.Events.Count() != 0)
            {
                Dictionary<string, object> eventData = new() { };
                foreach (Event eve in _context.Events)
                {
                    var eventUserData = new Dictionary<string, object>
                    {
                        { "reviews", _context.event_Attendance.Where(a => a.Event_Id == eve.Id).ToList() },
                        { "attendees", _context.Attendance.Where(a => a.Id == eve.Id).ToList() }
                    };

                    // Create the event details dictionary
                    var eventDetails = new Dictionary<string, object>
                    {
                        { "event", eve },
                        { "event data", eventUserData }
                    };
                    eventData.Add($"event {eve.Id}", eventDetails);
                }
                return Ok(eventData);
            }
            return BadRequest("there are no events in the database");
        }

        [AuthorizationFilter]
        [HttpGet("{id}")]
        public IActionResult getEvent(int id)
        {
            if (_context.Events.Count() != 0)
            {
                Event? eve = _context.Events.Where(e => e.Id == id).FirstOrDefault();
                if (eve == null)
                {
                    return BadRequest("event doesn't exist");
                }

                var eventUserData = new Dictionary<string, object>
                    {
                        { "reviews", _context.event_Attendance.Where(a => a.Event_Id == eve.Id).ToList() },
                        { "attendees", _context.Attendance.Where(a => a.Id == eve.Id).ToList() }
                    };

                // Create the event details dictionary
                var eventDetails = new Dictionary<string, object>
                    {
                        { "event", eve },
                        { "event data", eventUserData }
                    };
                Dictionary<string, object> eventData = new() { { $"event {eve.Id}", eventDetails } };
                return Ok(eventData);
            }
            return BadRequest("there are no events in the database");
        }


        [AdminFilter]
        [HttpPost]
        public IActionResult AddEvent([FromBody] Event eventToAdd)

        {
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

        [AdminFilter]
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
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

        [AdminFilter]
        [HttpPut]
        public IActionResult updateEvent([FromBody] Event updatedEvent)
        {
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
