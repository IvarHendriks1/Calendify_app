namespace CalendifyApp.Models
{
    public class EventAttendance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int? Rating { get; set; } // Nullable in case rating is optional
        public string Feedback { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }
    }
}
