namespace Project.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
        public double Rating { get; set; }
        public string Feedback { get; set; }
    }
}
