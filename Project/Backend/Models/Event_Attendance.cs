namespace CalendifyApp.Models
{
    public class Event_Attendance
    {   
        public int Id {get; set;}
        public int User_Id {get; set;}
        public int Event_Id {get; set;}
        public int Rating { get; set; }
        public required string Feedback { get; set; }
    }
}
