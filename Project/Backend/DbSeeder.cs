using Microsoft.EntityFrameworkCore;
using Project.Models;
public static class DbSeeder
{
    // Define the SeedData method as an extension method of ModelBuilder
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // Seed Admins
        modelBuilder.Entity<Admin>().HasData(
            new Admin { Id = 1, Username = "admin1", Password = "password123", Email = "admin1@example.com" },
            new Admin { Id = 2, Username = "admin2", Password = "password456", Email = "admin2@example.com" },
            new Admin { Id = 3, Username = "admin3", Password = "password789", Email = "admin3@example.com" },
            new Admin { Id = 4, Username = "admin4", Password = "password101", Email = "admin4@example.com" },
            new Admin { Id = 5, Username = "admin5", Password = "password102", Email = "admin5@example.com" },
            new Admin { Id = 6, Username = "admin6", Password = "password103", Email = "admin6@example.com" },
            new Admin { Id = 7, Username = "admin7", Password = "password104", Email = "admin7@example.com" },
            new Admin { Id = 8, Username = "admin8", Password = "password105", Email = "admin8@example.com" }
        );

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, First_name = "John", Last_name = "Doe", Email = "john.doe@example.com", Password = "password123", Recurring_days = "Monday, Wednesday" },
            new User { Id = 2, First_name = "Jane", Last_name = "Smith", Email = "jane.smith@example.com", Password = "password456", Recurring_days = "Tuesday, Thursday" },
            new User { Id = 3, First_name = "Michael", Last_name = "Johnson", Email = "michael.johnson@example.com", Password = "password789", Recurring_days = "Wednesday, Friday" },
            new User { Id = 4, First_name = "Emily", Last_name = "Davis", Email = "emily.davis@example.com", Password = "password101", Recurring_days = "Monday, Thursday" },
            new User { Id = 5, First_name = "Chris", Last_name = "Brown", Email = "chris.brown@example.com", Password = "password102", Recurring_days = "Tuesday, Friday" },
            new User { Id = 6, First_name = "Anna", Last_name = "Wilson", Email = "anna.wilson@example.com", Password = "password103", Recurring_days = "Monday, Friday" },
            new User { Id = 7, First_name = "David", Last_name = "Martinez", Email = "david.martinez@example.com", Password = "password104", Recurring_days = "Tuesday, Wednesday" },
            new User { Id = 8, First_name = "Laura", Last_name = "Garcia", Email = "laura.garcia@example.com", Password = "password105", Recurring_days = "Thursday, Friday" }
        );

        // Seed Events
        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Title = "Event 1", Description = "Description for Event 1", EventDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Location = "Location 1", Admin_approval = true },
            new Event { Id = 2, Title = "Event 2", Description = "Description for Event 2", EventDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0), Location = "Location 2", Admin_approval = true },
            new Event { Id = 3, Title = "Event 3", Description = "Description for Event 3", EventDate = DateTime.Now.AddDays(3), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Location = "Location 3", Admin_approval = true },
            new Event { Id = 4, Title = "Event 4", Description = "Description for Event 4", EventDate = DateTime.Now.AddDays(4), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 0, 0), Location = "Location 4", Admin_approval = true },
            new Event { Id = 5, Title = "Event 5", Description = "Description for Event 5", EventDate = DateTime.Now.AddDays(5), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(13, 0, 0), Location = "Location 5", Admin_approval = true },
            new Event { Id = 6, Title = "Event 6", Description = "Description for Event 6", EventDate = DateTime.Now.AddDays(6), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Location = "Location 6", Admin_approval = true },
            new Event { Id = 7, Title = "Event 7", Description = "Description for Event 7", EventDate = DateTime.Now.AddDays(7), StartTime = new TimeSpan(14, 30, 0), EndTime = new TimeSpan(16, 30, 0), Location = "Location 7", Admin_approval = true },
            new Event { Id = 8, Title = "Event 8", Description = "Description for Event 8", EventDate = DateTime.Now.AddDays(8), StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(18, 0, 0), Location = "Location 8", Admin_approval = true }
        );

        // Seed Attendance
        modelBuilder.Entity<Attendance>().HasData(
            new Attendance { Id = 1, UserId = 1, EventId = 1, IsPresent = true },
            new Attendance { Id = 2, UserId = 2, EventId = 2, IsPresent = false },
            new Attendance { Id = 3, UserId = 3, EventId = 3, IsPresent = true },
            new Attendance { Id = 4, UserId = 4, EventId = 4, IsPresent = true },
            new Attendance { Id = 5, UserId = 5, EventId = 5, IsPresent = false },
            new Attendance { Id = 6, UserId = 6, EventId = 6, IsPresent = true },
            new Attendance { Id = 7, UserId = 7, EventId = 7, IsPresent = true },
            new Attendance { Id = 8, UserId = 8, EventId = 8, IsPresent = false }
        );

        // Seed Reviews
        modelBuilder.Entity<Review>().HasData(
            new Reviews { Id = 1, UserId = 1, EventId = 1, Rating = 4.5, Feedback = "Great event!" },
            new Reviews { Id = 2, UserId = 2, EventId = 2, Rating = 3.0, Feedback = "It was okay." },
            new Reviews { Id = 3, UserId = 3, EventId = 3, Rating = 5.0, Feedback = "Amazing experience!" },
            new Reviews { Id = 4, UserId = 4, EventId = 4, Rating = 4.0, Feedback = "Good event, well organized." },
            new Reviews { Id = 5, UserId = 5, EventId = 5, Rating = 2.5, Feedback = "Not the best event." },
            new Reviews { Id = 6, UserId = 6, EventId = 6, Rating = 4.8, Feedback = "Really enjoyed it!" },
            new Reviews { Id = 7, UserId = 7, EventId = 7, Rating = 3.5, Feedback = "Decent, could be better." },
            new Reviews { Id = 8, UserId = 8, EventId = 8, Rating = 4.3, Feedback = "Pretty good event overall." }
        );
    }
}
