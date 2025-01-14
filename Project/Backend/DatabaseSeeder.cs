using CalendifyApp.Models;
using CalendifyApp.Utils;
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
                    new User { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = EncryptionHelper.EncryptPassword("password1"), RecurringDays = 1 },
                    new User { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Password = EncryptionHelper.EncryptPassword("password2"), RecurringDays = 2 },
                    new User { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Password = EncryptionHelper.EncryptPassword("password3"), RecurringDays = 0 },
                    new User { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Password = EncryptionHelper.EncryptPassword("password4"), RecurringDays = 1 },
                    new User { FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com", Password = EncryptionHelper.EncryptPassword("password5"), RecurringDays = 4 }
                );
                context.SaveChanges();
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
                context.SaveChanges();
            }

            if (!context.Attendance.Any())
            {
                context.Attendance.AddRange(
                    new Attendance { UserId = context.Users.First(u => u.FirstName == "John").Id, Date = DateTime.Now.AddDays(1) },
                    new Attendance { UserId = context.Users.First(u => u.FirstName == "Jane").Id, Date = DateTime.Now.AddDays(2) },
                    new Attendance { UserId = context.Users.First(u => u.FirstName == "Alice").Id, Date = DateTime.Now.AddDays(3) },
                    new Attendance { UserId = context.Users.First(u => u.FirstName == "Bob").Id, Date = DateTime.Now.AddDays(4) },
                    new Attendance { UserId = context.Users.First(u => u.FirstName == "Charlie").Id, Date = DateTime.Now.AddDays(5) }
                );
                context.SaveChanges();
            }

            if (!context.EventAttendances.Any())
            {
                context.EventAttendances.AddRange(
                    new EventAttendance { UserId = context.Users.First(u => u.FirstName == "John").Id, EventId = context.Events.First(e => e.Title == "Meeting").Id, Rating = 5, Feedback = "Great event!" },
                    new EventAttendance { UserId = context.Users.First(u => u.FirstName == "Jane").Id, EventId = context.Events.First(e => e.Title == "Workshop").Id, Rating = 4, Feedback = "Informative session." },
                    new EventAttendance { UserId = context.Users.First(u => u.FirstName == "Alice").Id, EventId = context.Events.First(e => e.Title == "Conference").Id, Rating = 5, Feedback = "Very engaging." },
                    new EventAttendance { UserId = context.Users.First(u => u.FirstName == "Bob").Id, EventId = context.Events.First(e => e.Title == "Presentation").Id, Rating = 3, Feedback = "Could be better." },
                    new EventAttendance { UserId = context.Users.First(u => u.FirstName == "Charlie").Id, EventId = context.Events.First(e => e.Title == "Training").Id, Rating = 4, Feedback = "Well organized." }
                );
                context.SaveChanges();
            }

            if (!context.Admin.Any())
            {
                context.Admin.AddRange(
                    new Admin { Username = "admin1", Password = EncryptionHelper.EncryptPassword("adminpass1"), Email = "admin1@example.com" },
                    new Admin { Username = "admin2", Password = EncryptionHelper.EncryptPassword("adminpass2"), Email = "admin2@example.com" },
                    new Admin { Username = "admin3", Password = EncryptionHelper.EncryptPassword("adminpass3"), Email = "admin3@example.com" },
                    new Admin { Username = "admin4", Password = EncryptionHelper.EncryptPassword("adminpass4"), Email = "admin4@example.com" },
                    new Admin { Username = "admin5", Password = EncryptionHelper.EncryptPassword("adminpass5"), Email = "admin5@example.com" }
                );
                context.SaveChanges();
            }
        }
    }
}
