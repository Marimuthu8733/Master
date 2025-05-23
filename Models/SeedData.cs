using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Tour_Management_.Net_8.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TourDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TourDbContext>>());

            // Look for any users.
            if (context.Users.Any() || context.Tours.Any() || context.Bookings.Any())
                return; // DB has been seeded

            context.Users.AddRange(
                new UserInfo {
                    Email = "admin@example.com", FirstName = "Admin", LastName = "User", Gender = "Male", Password = "admin123", Dob = new DateTime(1990,1,1), Street = "123 Main St", City = "CityA", State = "StateA"
                },
                new UserInfo {
                    Email = "user1@example.com", FirstName = "User", LastName = "One", Gender = "Female", Password = "user123", Dob = new DateTime(1995,5,5), Street = "456 Side St", City = "CityB", State = "StateB"
                }
            );

            context.Tours.AddRange(
                new Tour {
                    TourName = "Beach Paradise", Place = "Goa", Days = 5, Price = 15000, Locations = "Goa Beach, Fort Aguada", TourInfo = "Enjoy the beaches of Goa", Pic = "goa.jpg"
                },
                new Tour {
                    TourName = "Mountain Adventure", Place = "Manali", Days = 7, Price = 20000, Locations = "Solang Valley, Rohtang Pass", TourInfo = "Explore the mountains", Pic = "manali.jpg"
                }
            );

            context.Bookings.AddRange(
                new Booking {
                    BookingId = 1, TourId = 1, TourName = "Beach Paradise", Place = "Goa", Email = "user1@example.com", FirstName = "User"
                }
            );

            context.SaveChanges();
        }
    }
}
