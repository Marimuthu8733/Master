using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tour_Management_.Net_8.Models
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfo>> GetAllAsync();
        Task<UserInfo?> GetByEmailAsync(string email);
        Task<UserInfo> AddAsync(UserInfo user);
        Task<UserInfo?> UpdateAsync(string email, UserInfo user);
        Task<bool> DeleteAsync(string email);
        Task<UserInfo?> LoginAsync(string email, string password);
    }

    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour?> GetByIdAsync(int id);
        Task<Tour> AddAsync(Tour tour);
        Task<Tour?> UpdateAsync(int id, Tour tour);
        Task<bool> DeleteAsync(int id);
    }

    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id); // id is BookingId
        Task<Booking> AddAsync(Booking booking);
        Task<Booking?> UpdateAsync(int id, Booking booking); // id is BookingId
        Task<bool> DeleteAsync(int id); // id is BookingId
        Task<IEnumerable<Booking>> GetByUserEmailAsync(string email);
    }
}
