using Microsoft.EntityFrameworkCore;


namespace CalendifyApp.Models
{
    public class MyContext : DbContext
    {
        private readonly IConfiguration configuration;

        public MyContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Configure the database to use SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        }

        // DbSets for each table in your database
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Event_Attendance> event_Attendance { get; set; }
        public DbSet<User> Users { get; set; }

        // OnModelCreating method to configure the model and seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Call the seed data method to populate the database with dummy data
            modelBuilder.SeedData();
        }
    }
}
