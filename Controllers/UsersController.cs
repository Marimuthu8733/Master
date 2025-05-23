using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tour_Management_.Net_8.Models;

namespace Tour_Management_.Net_8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _service.GetByEmailAsync(email);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInfo user)
        {
            try
            {
                var created = await _service.AddAsync(user);
                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, UserInfo user)
        {
            var updated = await _service.UpdateAsync(email, user);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var deleted = await _service.DeleteAsync(email);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _service.LoginAsync(req.Email, req.Password);
            return user == null ? Unauthorized() : Ok(user);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
