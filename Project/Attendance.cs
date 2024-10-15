namespace CalendifyApp
{
    public class Attendance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public bool IsPresent { get; set; }
    }
}
