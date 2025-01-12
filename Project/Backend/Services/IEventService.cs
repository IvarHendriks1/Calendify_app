using CalendifyApp.Models;
using System;
using System.Collections.Generic;

namespace CalendifyApp.Services
{
    public interface IEventService
    {
        List<Event> GetAllEvents();
        Event? GetEventById(int id);
        Task<Event> AddEvent(CreateEventDto newEvent);
        bool UpdateEvent(int id, Event updatedEvent);
        bool DeleteEvent(int id);
        List<EventAttendance> GetAllReviews();
        bool AddReview(EventAttendance review);
        List<Event> SearchEvents(string? title, string? location, DateTime? startDate, DateTime? endDate);
    }
}
