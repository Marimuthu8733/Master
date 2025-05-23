using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tour_Management_.Net_8.Models;

namespace Tour_Management_.Net_8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingsController(IBookingService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) // id is BookingId
        {
            var booking = await _service.GetByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            var created = await _service.AddAsync(booking);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Booking booking) // id is BookingId
        {
            var updated = await _service.UpdateAsync(id, booking);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // id is BookingId
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetByUserEmail(string email)
        {
            var bookings = await _service.GetByUserEmailAsync(email);
            return Ok(bookings);
        }
    }
}
