using Microsoft.EntityFrameworkCore;
using CalendifyApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Register the MyContext DbContext with a SQLite connection string
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlite("Data Source=calendify.db")); // Using SQLite database

var app = builder.Build();

app.Urls.Add("http://localhost:5001");

app.MapGet("/hi", () => "Hello pleps!");

// Seed the database on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyContext>();
    dbContext.Database.EnsureCreated(); // Ensure the database is created

    // Call the Seed method from the DbSeeder class
    DbSeeder.Seed(dbContext); // Pass the context directly to the seeder
}

// Run the application
app.Run();
