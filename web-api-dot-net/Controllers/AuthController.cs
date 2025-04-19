using Microsoft.AspNetCore.Mvc;
using web_api_dot_net.Data.Repositories;
using web_api_dot_net.Models;

namespace web_api_dot_net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var token = _authService.Authenticate(user.Username, user.Password);
            if (token == null) return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
