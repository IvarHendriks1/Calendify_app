using System.Collections.Generic;

namespace CalendifyApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public bool AdminApproval { get; set; }

        public ICollection<EventAttendance> EventAttendances { get; set; }
    }
}
