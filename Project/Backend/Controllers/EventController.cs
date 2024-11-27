using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Models;
using CalendifyApp.Services;
using System.Linq;
using CalendifyApp.Filters;
using System.Data.Entity.Core.Objects;


namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/Events")]
    //[controller]
    public class EventController : Controller
    {

        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [AuthorizationFilter]
        [HttpGet]
        public IActionResult getEvents()
        {
            Dictionary<string, object>? events = _eventService.allEvents();
            if (events == null) return BadRequest("there are no events");
            return Ok(events);
        }

        [AuthorizationFilter]
        [HttpGet("{id}")]
        public IActionResult getEvent(int id)
        {
            object? eve = _eventService.GetOneEvent(id);
            if (eve == null) return BadRequest($"no event with id {id}");
            return Ok(eve);
        }


        [AdminFilter]
        [HttpPost]
        public IActionResult AddEvent([FromBody] Event eventToAdd)

        {
            string result = _eventService.postEvent(eventToAdd);
            if (result == "Event has been added succesfully") return Ok($"Event has been added succesfully\n{eventToAdd}");
            return BadRequest(result);
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

        [AuthorizationFilter]
        [HttpGet("review")]
        public IActionResult watchReviews()
        {
            if (_context.event_Attendance.Count() == 0)
            {
                return BadRequest("there are no reviews");
            }

            return Ok(_context.event_Attendance.ToList());
        }


        [AuthorizationFilter]
        [HttpPost("review")]
        public IActionResult addReview([FromBody] Event_Attendance review)
        {
            if (_context.Events.Count() == 0)
            {
                return BadRequest("There are no events");
            }

            Event? eve = _context.Events.Where(e => e.Id == review.Event_Id).FirstOrDefault();
            if (eve == null)
            {
                return BadRequest("event doesn't exist");
            }
            if (_context.event_Attendance.Contains(review) ||
            _context.event_Attendance.FirstOrDefault(r => r.User_Id == review.User_Id) != null)
            {
                return BadRequest("review already exists");
            }
            _context.event_Attendance.Add(review);
            _context.SaveChanges();
            return Ok(new { message = "Review has been added", user_review = review });
        }


    }
}
