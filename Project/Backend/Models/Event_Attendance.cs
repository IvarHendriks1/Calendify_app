﻿namespace CalendifyApp.Models
{
    public class Event_Attendance
    {
        public int Event_AttendanceId { get; set; }
        public int Rating { get; set; }
        public required string Feedback { get; set; }
        public required User User { get; set; }
        public required Event Event { get; set; }
    }
}
