using CalendifyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendifyApp.Services
{
    public class EventService : IEventService
    {
        private readonly MyContext _context;

        public EventService(MyContext context)
        {
            _context = context;
        }

        public List<Event> GetAllEvents()
        {
            return _context.Events.Include(e => e.EventAttendances).ToList();
        }

        public Event? GetEventById(int id)
        {
            return _context.Events
                .Include(e => e.EventAttendances)
                .ThenInclude(ea => ea.User)
                .FirstOrDefault(e => e.Id == id);
        }

        public async Task<Event> AddEvent(DTOEvent newEvent)
        {
            // Map the DTO to the Event entity
            var eventToAdd = new Event
            {
                Title = newEvent.Title,
                Description = newEvent.Description,
                Date = newEvent.Date,
                StartTime = newEvent.StartTime,
                EndTime = newEvent.EndTime,
                Location = newEvent.Location,
                AdminApproval = newEvent.AdminApproval
            };

            // Add event to the database
            _context.Events.Add(eventToAdd);
            await _context.SaveChangesAsync();

            return eventToAdd; // Return the saved event
        }

        public bool UpdateEvent(int id, Event updatedEvent)
        {
            var existingEvent = _context.Events.FirstOrDefault(e => e.Id == id);
            if (existingEvent == null) return false;

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.StartTime = updatedEvent.StartTime;
            existingEvent.EndTime = updatedEvent.EndTime;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.AdminApproval = updatedEvent.AdminApproval;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteEvent(int id)
        {
            var eventToDelete = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventToDelete == null) return false;

            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();
            return true;
        }

        public List<EventAttendance> GetAllReviews()
        {
            return _context.EventAttendances.Include(ea => ea.User).ToList();
        }

        public bool AddReview(EventAttendance review)
        {
            try
            {
                _context.EventAttendances.Add(review);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Event> SearchEvents(string? title, string? location, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(e => e.Title.Contains(title));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            if (startDate.HasValue)
                query = query.Where(e => e.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.Date <= endDate.Value);

            return query.Include(e => e.EventAttendances).ToList();
        }
    }
}
