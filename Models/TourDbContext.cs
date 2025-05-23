using Microsoft.EntityFrameworkCore;

namespace Tour_Management_.Net_8.Models
{
    public class TourDbContext : DbContext
    {
        public TourDbContext(DbContextOptions<TourDbContext> options) : base(options) { }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserInfo
            modelBuilder.Entity<UserInfo>().HasKey(u => u.Email);
            modelBuilder.Entity<UserInfo>().Property(u => u.Gender).HasMaxLength(10);
            modelBuilder.Entity<UserInfo>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<UserInfo>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<UserInfo>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<UserInfo>().Property(u => u.City).IsRequired();
            modelBuilder.Entity<UserInfo>().Property(u => u.State).IsRequired();
            modelBuilder.Entity<UserInfo>().Property(u => u.Street).IsRequired();
            modelBuilder.Entity<UserInfo>().Property(u => u.Dob).IsRequired();

            // Tour
            modelBuilder.Entity<Tour>().HasKey(t => t.TourId);
            modelBuilder.Entity<Tour>().Property(t => t.TourName).IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.Place).IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.Days).IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.Price).IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.Locations).IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.TourInfo).IsRequired();

            // Booking
            modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);
        }
    }
}
