using CalendifyApp.Models;
using CalendifyApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IEventAttendanceService, EventAttendanceService>();
builder.Services.AddDistributedMemoryCache(); // For session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register the MyContext DbContext with a SQLite connection string
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlite("Data Source=calendify.db")); // Using SQLite database

var app = builder.Build();
app.MapControllers();

app.UseSession();


app.Urls.Add("http://localhost:5001");

app.MapGet("/hi", () => "Hello pleps!");


// Ensure that MyContext is injected via dependency injection, not instantiated manually.

// Run the application

app.Run();

