using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tour_Management_.Net_8.Models;

namespace Tour_Management_.Net_8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _service;
        public ToursController(ITourService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tour = await _service.GetByIdAsync(id);
            return tour == null ? NotFound() : Ok(tour);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tour tour) => Ok(await _service.AddAsync(tour));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tour tour)
        {
            var updated = await _service.UpdateAsync(id, tour);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
