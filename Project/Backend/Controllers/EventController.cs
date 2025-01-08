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
            return Ok("got here");
            Dictionary<string, object>? events = _eventService.allEvents();
            if (events == null) return BadRequest("there are no events");
            return Ok("events");
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
            Event? deletedEvent = _eventService.deleteEvent(id);
            if (deletedEvent != null)
                return Ok(new
                {
                    message = $"Event with id {id} has been deleted successfully",
                    deleted_event = deletedEvent
                });
            return BadRequest($"event {id} doesn't exists");
        }


        [AdminFilter]
        [HttpPut]
        public IActionResult updateEvent([FromBody] Event updatedEvent)
        {
            Event? eventToUpdate = _eventService.putEvent(updatedEvent);
            if (eventToUpdate != null)
            {
                return Ok(new
                {
                    message = $"Event with id {updatedEvent.Id} has been updated successfully",
                    updatedVersion = updatedEvent,
                    olderVersion = eventToUpdate
                });
            }
            return BadRequest($"event {updatedEvent.Id} doesn't exists");
        }

        [AuthorizationFilter]
        [HttpGet("review")]
        public IActionResult watchReviews()
        {
            List<EventAttendance>? reviews = _eventService.allReviews();
            if (reviews != null) return Ok(reviews.ToString());
            return BadRequest($"There are no reviews");
        }


        [AuthorizationFilter]
        [HttpPost("review")]
        public IActionResult addReview([FromBody] EventAttendance review)
        {
            string result = _eventService.PostReview(review);
            if (result == "succes") return Ok(new { message = "review added succesfully", added_review = review });
            return BadRequest(result);
        }
    }
}
