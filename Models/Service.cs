using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tour_Management_.Net_8.Models
{
    public class UserService : IUserService
    {
        private readonly TourDbContext _context;
        public UserService(TourDbContext context) => _context = context;

        public async Task<IEnumerable<UserInfo>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<UserInfo?> GetByEmailAsync(string email) => await _context.Users.FindAsync(email);
        public async Task<UserInfo> AddAsync(UserInfo user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new InvalidOperationException("User already exists.");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<UserInfo?> UpdateAsync(string email, UserInfo user)
        {
            var existing = await _context.Users.FindAsync(email);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(string email)
        {
            var user = await _context.Users.FindAsync(email);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<UserInfo?> LoginAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }

    public class TourService : ITourService
    {
        private readonly TourDbContext _context;
        public TourService(TourDbContext context) => _context = context;

        public async Task<IEnumerable<Tour>> GetAllAsync() => await _context.Tours.ToListAsync();
        public async Task<Tour?> GetByIdAsync(int id) => await _context.Tours.FindAsync(id);
        public async Task<Tour> AddAsync(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            return tour;
        }
        public async Task<Tour?> UpdateAsync(int id, Tour tour)
        {
            var existing = await _context.Tours.FindAsync(id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(tour);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) return false;
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class BookingService : IBookingService
    {
        private readonly TourDbContext _context;
        public BookingService(TourDbContext context) => _context = context;

        public async Task<IEnumerable<Booking>> GetAllAsync() => await _context.Bookings.ToListAsync();
        public async Task<Booking?> GetByIdAsync(int id) => await _context.Bookings.FindAsync(id); // id is BookingId
        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async Task<Booking?> UpdateAsync(int id, Booking booking)
        {
            var existing = await _context.Bookings.FindAsync(id); // id is BookingId
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(booking);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id); // id is BookingId
            if (booking == null) return false;
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Booking>> GetByUserEmailAsync(string email)
        {
            return await _context.Bookings.Where(b => b.Email == email).ToListAsync();
        }
    }
}
