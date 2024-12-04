using CalendifyApp.Models;

namespace CalendifyApp.Services
{
    public interface IEventAttendanceService
    {
        bool AttendEvent(AttendanceDto attendance);
        List<object> GetEventAttendees(int eventId);
        List<object> GetEventsByUser(int userId);
        bool RemoveAttendance(AttendanceDto attendance);
    }
}
