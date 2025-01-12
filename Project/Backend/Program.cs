using CalendifyApp.Models;
using CalendifyApp.Seeders; // Import the seeder namespace
using CalendifyApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Logging.AddConsole();
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

// Seed the database during application startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyContext>();
    context.Database.EnsureCreated(); // Ensure the database is created
    DatabaseSeeder.Seed(context);    // Call the seeder to populate the database
}

app.UseSession();

app.MapControllers();

app.Urls.Add("http://localhost:5001");

app.MapGet("/hi", () => "Hello pleps!");

// Run the application
app.Run();
