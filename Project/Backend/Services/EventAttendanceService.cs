using CalendifyApp.Models;

namespace CalendifyApp.Services
{
    public class EventAttendanceService : IEventAttendanceService
    {
        private readonly MyContext _context;

        public EventAttendanceService(MyContext context)
        {
            _context = context;
        }

        public bool AttendEvent(AttendanceDto attendance)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == attendance.UserId);
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);

            if (user == null || eventEntity == null)
                return false;

            if (eventEntity.Date.ToDateTime(eventEntity.StartTime) < DateTime.Now)
                throw new InvalidOperationException("The event has already started or ended.");

            var existingAttendance = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            if (existingAttendance != null)
                throw new InvalidOperationException("You are already registered for this event.");

            var newAttendance = new Attendance
            {
                UserId = attendance.UserId,
                Date = eventEntity.Date
            };

            _context.Attendance.Add(newAttendance);
            _context.SaveChanges();

            return true;
        }

        public List<object> GetEventAttendees(int eventId)
        {
            var eventDate = _context.Events
                .Where(e => e.Id == eventId)
                .Select(e => e.Date)
                .FirstOrDefault();

            if (eventDate == null)
                return null;

            return _context.Attendance
                .Where(a => a.Date == eventDate)
                .Join(
                    _context.Users,
                    attendance => attendance.UserId,
                    user => user.Id,
                    (attendance, user) => new
                    {
                        attendance.UserId,
                        UserName = $"{user.First_name} {user.Last_name}"
                    }
                )
                .ToList<object>();
        }

        public List<object> GetEventsByUser(int userId)
        {
            var userExists = _context.Users.Any(u => u.Id == userId);

            if (!userExists)
                return null;

            return _context.Attendance
                .Where(a => a.UserId == userId)
                .Join(
                    _context.Events,
                    attendance => attendance.Date,
                    eventEntity => eventEntity.Date,
                    (attendance, eventEntity) => new
                    {
                        eventEntity.Id,
                        eventEntity.Title,
                        eventEntity.Date,
                        eventEntity.StartTime,
                        eventEntity.EndTime
                    }
                )
                .ToList<object>();
        }

        public bool RemoveAttendance(AttendanceDto attendance)
        {
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);

            if (eventEntity == null)
                return false;

            var attendanceRecord = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            if (attendanceRecord == null)
                return false;

            _context.Attendance.Remove(attendanceRecord);
            _context.SaveChanges();

            return true;
        }
    }
}
