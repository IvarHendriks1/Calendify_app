using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Models;
using CalendifyApp.Services;
using CalendifyApp.Filters;
using System.Collections.Generic;

namespace CalendifyApp.Controllers
{
    [ApiController]
    [Route("api/Events")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = _eventService.GetAllEvents();
            if (events == null || !events.Any())
                return NotFound("No events available.");
            
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var eventDetails = _eventService.GetEventById(id);
            if (eventDetails == null)
                return NotFound($"No event found with ID {id}.");
            
            return Ok(eventDetails);
        }

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] Event eventToAdd)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            Event createdevent = await _eventService.AddEvent(eventToAdd);
            return CreatedAtAction(nameof(GetEventById), new {id = createdevent.Id }, createdevent);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            var result = _eventService.UpdateEvent(id, updatedEvent);
            if (result)
                return Ok($"Event with ID {id} updated successfully.");
            
            return NotFound($"Event with ID {id} not found.");
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var result = _eventService.DeleteEvent(id);
            if (result)
                return Ok($"Event with ID {id} deleted successfully.");
            
            return NotFound($"Event with ID {id} not found.");
        }

        [HttpGet("reviews")]
        public IActionResult GetAllReviews()
        {
            var reviews = _eventService.GetAllReviews();
            if (reviews == null || !reviews.Any())
                return NotFound("No reviews found.");
            
            return Ok(reviews);
        }

        [HttpPost("review")]
        public IActionResult AddReview([FromBody] EventAttendance review)
        {
            var result = _eventService.AddReview(review);
            if (result)
                return Ok("Review added successfully.");
            
            return BadRequest("Failed to add the review.");
        }

        [HttpGet("search")]
        public IActionResult SearchEvents(string? title = null, string? location = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var events = _eventService.SearchEvents(title, location, startDate, endDate);
            if (events == null || !events.Any())
                return NotFound("No matching events found.");
            
            return Ok(events);
        }
    }
}
