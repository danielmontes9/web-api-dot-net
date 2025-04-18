using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using web_api_dot_net.Data.Repositories;
using web_api_dot_net.Models;

namespace web_api_dot_net.Services
{
    public class AuthService : IAuthService
    {
        // Mock user - Replace with DB logic in real apps
        private readonly List<User> _users = new()
    {
        new User { Username = "admin", Password = "password" }
    };

        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user == null) return null;

            // Create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
