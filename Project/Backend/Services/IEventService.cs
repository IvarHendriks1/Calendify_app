using CalendifyApp.Models;

namespace CalendifyApp.Services;
public interface IEventService
{
    public Dictionary<string, object>? allEvents();
    public object? GetOneEvent(int id);
    public string postEvent(Event eventToAdd);
    public Event? putEvent(Event eve);
    public Event? deleteEvent(int id);
    public List<EventAttendance>? allReviews();
    public string PostReview(EventAttendance review);
}
