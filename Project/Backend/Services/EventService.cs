using CalendifyApp.Models;
using CalendifyApp.Utils;
using CalendifyApp.Controllers;

namespace CalendifyApp.Services;

public class EventService : IEventService
{
    private readonly MyContext _context;

    public EventService(MyContext context)
    {
        _context = context;
    }

    public Dictionary<string, object>? allEvents()
    {
        if (_context.Events.Count() != 0)
        {
            Dictionary<string, object> eventData = new() { };
            foreach (Event eve in _context.Events)
            {
                var eventUserData = new Dictionary<string, object>
                    {
                        { "reviews", _context.EventAttendance.Where(a => a.Event_Id == eve.Id).ToList() },
                        { "attendees", _context.Attendance.Where(a => a.Id == eve.Id).ToList() }
                    };

                var eventDetails = new Dictionary<string, object>
                    {
                        { "event", eve },
                        { "event data", eventUserData }
                    };
                eventData.Add($"event {eve.Id}", eventDetails);
            }
            return eventData;
        }
        return null;
    }
    public object? GetOneEvent(int id)
    {
        if (_context.Events.Count() != 0)
        {
            Event? eve = _context.Events.Where(e => e.Id == id).FirstOrDefault();
            if (eve == null)
            {
                return null;
            }

            var eventUserData = new Dictionary<string, object>
                    {
                        { "reviews", _context.EventAttendance.Where(a => a.Event_Id == eve.Id).ToList() },
                        { "attendees", _context.Attendance.Where(a => a.Id == eve.Id).ToList() }
                    };

            // Create the event details dictionary
            var eventDetails = new Dictionary<string, object>
                    {
                        { "event", eve },
                        { "event data", eventUserData }
                    };
            Dictionary<string, object> eventData = new() { { $"event {eve.Id}", eventDetails } };
            return eventData;
        }
        return null;
    }
    public string postEvent(Event eventToAdd)
    {
        if (eventToAdd is not Event)
        {
            return "given event could not be added";
        }

        Event? eventExists = _context.Events.FirstOrDefault(e => e.Id == eventToAdd.Id);
        if (eventExists is null)
        {
            _context.Events.Add(eventToAdd);
            if (_context.SaveChanges() == 1) return "Event has been added succesfully";
        }
        else if (eventExists is not null)
        {
            return "event already exists";
        }
        return "given event could not be added";
    }

    public Event? putEvent(Event eve)
    {
        Event? eventToUpdate = _context.Events.FirstOrDefault(e => e.Id == eve.Id);
        if (eventToUpdate is not null)
        {
            eventToUpdate.Title = eve.Title;
            eventToUpdate.Description = eve.Description;
            eventToUpdate.Date = eve.Date;
            eventToUpdate.StartTime = eve.StartTime;
            eventToUpdate.EndTime = eve.EndTime;
            eventToUpdate.Location = eve.Location;
            eventToUpdate.Admin_approval = eve.Admin_approval;

            _context.SaveChanges();
            return eventToUpdate;
        }
        return null;
    }

    public Event? deleteEvent(int id)
    {
        var eventToDelete = _context.Events.FirstOrDefault(e => e.Id == id);
        if (eventToDelete is not null)
        {
            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();
            return eventToDelete;
        }
        return null;

    }
    public List<EventAttendance>? allReviews()
    {
        if (_context.EventAttendance.Count() == 0) return null;
        return _context.EventAttendance.ToList();
    }
    public string PostReview(EventAttendance review)
    {
        if (_context.Events.Count() == 0)
        {
            return "There are no events";
        }

        Event? eve = _context.Events.Where(e => e.Id == review.Event_Id).FirstOrDefault();
        if (eve == null)
        {
            return "event doesn't exist";
        }
        if (_context.EventAttendance.Contains(review) ||
        _context.EventAttendance.FirstOrDefault(r => r.User_Id == review.User_Id) != null)
        {
            return "review already exists";
        }
        _context.EventAttendance.Add(review);
        _context.SaveChanges();
        return "succes";
    }
}