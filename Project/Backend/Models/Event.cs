using System.Collections.Generic;

namespace CalendifyApp.Models
{
    public class Event
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public required string Location { get; set; }

        public bool Admin_approval { get; set; }
    }
}
