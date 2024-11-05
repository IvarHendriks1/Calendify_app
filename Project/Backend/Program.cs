using CalendifyApp.Models; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register the MyContext DbContext with a SQLite connection string
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlite("Data Source=calendify.db")); // Using SQLite database

var app = builder.Build();

app.Urls.Add("http://localhost:5001");

app.MapGet("/hi", () => "Hello pleps!");

// Ensure that MyContext is injected via dependency injection, not instantiated manually.

// Run the application
app.Run();
