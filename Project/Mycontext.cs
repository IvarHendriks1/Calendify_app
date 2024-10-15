using Microsoft.EntityFrameworkCore;

namespace CalendifyApp
{
    public class MyContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendance> Attendance { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
