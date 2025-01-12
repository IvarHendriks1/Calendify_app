using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CalendifyApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public bool AdminApproval { get; set; }

        public ICollection<EventAttendance> EventAttendances { get; set; }
    }
}
