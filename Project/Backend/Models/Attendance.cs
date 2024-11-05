namespace CalendifyApp.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public DateOnly Date {get; set; }
    }
}
