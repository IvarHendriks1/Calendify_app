namespace CalendifyApp.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly Date {get; set; }
        public Attendance() { }
        public Attendance(int id, int userId, DateOnly date)
        {
            Id = id;
            UserId = userId;
            Date = date;
        }
    }
}
