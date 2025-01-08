using CalendifyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CalendifyApp.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(MyContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "password1", RecurringDays = "Monday" },
                    new User { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Password = "password2", RecurringDays = "Tuesday" },
                    new User { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Password = "password3", RecurringDays = "Wednesday" },
                    new User { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Password = "password4", RecurringDays = "Thursday" },
                    new User { FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com", Password = "password5", RecurringDays = "Friday" }
                );
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(
                    new Event { Title = "Meeting", Description = "Team meeting", Date = DateTime.Now.AddDays(1), StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(12), Location = "Room 101", AdminApproval = true },
                    new Event { Title = "Workshop", Description = "Coding workshop", Date = DateTime.Now.AddDays(2), StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(16), Location = "Room 202", AdminApproval = true },
                    new Event { Title = "Conference", Description = "Tech conference", Date = DateTime.Now.AddDays(3), StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(17), Location = "Auditorium", AdminApproval = true },
                    new Event { Title = "Presentation", Description = "Project presentation", Date = DateTime.Now.AddDays(4), StartTime = TimeSpan.FromHours(11), EndTime = TimeSpan.FromHours(13), Location = "Room 303", AdminApproval = true },
                    new Event { Title = "Training", Description = "Skill training", Date = DateTime.Now.AddDays(5), StartTime = TimeSpan.FromHours(15), EndTime = TimeSpan.FromHours(17), Location = "Training Hall", AdminApproval = true }
                );
            }

            if (!context.Attendance.Any())
            {
                context.Attendance.AddRange(
                    new Attendance { UserId = 1, Date = DateTime.Now.AddDays(1) },
                    new Attendance { UserId = 2, Date = DateTime.Now.AddDays(2) },
                    new Attendance { UserId = 3, Date = DateTime.Now.AddDays(3) },
                    new Attendance { UserId = 4, Date = DateTime.Now.AddDays(4) },
                    new Attendance { UserId = 5, Date = DateTime.Now.AddDays(5) }
                );
            }

            if (!context.EventAttendances.Any())
            {
                context.EventAttendances.AddRange(
                    new EventAttendance { UserId = 1, EventId = 1, Rating = 5, Feedback = "Great event!" },
                    new EventAttendance { UserId = 2, EventId = 2, Rating = 4, Feedback = "Informative session." },
                    new EventAttendance { UserId = 3, EventId = 3, Rating = 5, Feedback = "Very engaging." },
                    new EventAttendance { UserId = 4, EventId = 4, Rating = 3, Feedback = "Could be better." },
                    new EventAttendance { UserId = 5, EventId = 5, Rating = 4, Feedback = "Well organized." }
                );
            }

            if (!context.Admin.Any())
            {
                context.Admin.AddRange(
                    new Admin { Username = "admin1", Password = "adminpass1", Email = "admin1@example.com" },
                    new Admin { Username = "admin2", Password = "adminpass2", Email = "admin2@example.com" },
                    new Admin { Username = "admin3", Password = "adminpass3", Email = "admin3@example.com" },
                    new Admin { Username = "admin4", Password = "adminpass4", Email = "admin4@example.com" },
                    new Admin { Username = "admin5", Password = "adminpass5", Email = "admin5@example.com" }
                );
            }

            context.SaveChanges();
        }
    }
}
