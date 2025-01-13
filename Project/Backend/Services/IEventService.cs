using CalendifyApp.Models;
using System;
using System.Collections.Generic;

namespace CalendifyApp.Services
{
    public interface IEventService
    {
        List<DetailedEventDTO> GetAllEvents(); // Return DTOEvent instead of Event
        DetailedEventDTO? GetEventById(int id); // Return DTOEvent instead of Event
        Task<Event> AddEvent(DTOEvent newEvent);
        bool UpdateEvent(int id, Event updatedEvent);
        bool DeleteEvent(int id);
        List<EventAttendance> GetAllReviews();
        bool AddReview(EventAttendance review);
        List<Event> SearchEvents(string? title, string? location, DateTime? startDate, DateTime? endDate);
    }
}
 