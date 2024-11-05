using CalendifyApp.Models;
using Microsoft.EntityFrameworkCore;
using CalendifyApp.Utils;

public static class DbSeeder
{
    public static void Seed(MyContext context)
    {
        // Check if Admin exists, if not, add one
        if (!context.Admin.Any())
        {
            context.Admin.Add(new Admin
            {
                Id = 1,
                UserName = "admin",
                Password = EncryptionHelper.EncryptPassword("password123"), // Consider hashing passwords in a real app
                Email = "admin@example.com"
            });
            context.SaveChanges(); // Save changes to the database
        }

        // Check if Users exist, if not, add them
        if (!context.Users.Any())
        {
            context.Users.AddRange(new[]
            {
                new User
                {
                    Id = 1,
                    First_name = "John",
                    Last_name = "Doe",
                    Email = "johndoe@example.com",
                    Password = EncryptionHelper.EncryptPassword("password"), // Consider hashing passwords in a real app
                    Recurring_days = "Monday,Wednesday,Friday"
                },
                new User
                {
                    Id = 2,
                    First_name = "Jane",
                    Last_name = "Smith",
                    Email = "janesmith@example.com",
                    Password = EncryptionHelper.EncryptPassword("password"), // Consider hashing passwords in a real app
                    Recurring_days = "Tuesday,Thursday"
                }
            });

            context.SaveChanges(); // Save changes to the database
        }

        // Check if Events exist, if not, add them
        if (!context.Events.Any())
        {
            context.Events.AddRange(new[]
            {
                new Event
                {
                    Id = 1,
                    Title = "Morning Yoga",
                    Description = "A relaxing yoga session",
                    Date = new DateOnly(2024, 11, 10),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 0),
                    Location = "Gym",
                    Admin_approval = true
               },
                new Event
                {
                    Id = 2,
                    Title = "Team Meeting",
                    Description = "Weekly team meeting",
                    Date = new DateOnly(2024, 11, 15),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(11, 0),
                    Location = "Conference Room",
                    Admin_approval = false
                }
            });

            context.SaveChanges(); // Save changes to the database
        }

        // Check if Attendance records exist, if not, add them
        if (!context.Attendance.Any())
        {
            context.Attendance.AddRange(new[]
            {
                new Attendance
                {
                    Id = 1,
                    UserId = 1,
                    Date = new DateOnly(2024, 11, 5)
                },
                new Attendance
                {
                    Id = 2,
                    UserId = 2,
                    Date = new DateOnly(2024, 11, 6)
                }
            });

            context.SaveChanges(); // Save changes to the database
        }

        // Check if Event_Attendance records exist, if not, add them
        if (!context.event_Attendance.Any())
        {
            context.event_Attendance.AddRange(new[]
            {
                new Event_Attendance
                {
                    Id = 1,
                    User_Id = 1,
                    Event_Id = 1,
                    Rating = 5,
                    Feedback = "Great event!"
                },
                new Event_Attendance
                {
                    Id = 2,
                    User_Id = 2,
                    Event_Id = 2,
                    Rating = 4,
                    Feedback = "Very informative."
                }
            });

            context.SaveChanges(); // Save changes to the database
        }
    }
}
