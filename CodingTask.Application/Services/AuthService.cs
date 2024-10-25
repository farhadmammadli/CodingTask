using CodingTask.Application.Exceptions;
using CodingTask.Application.Interfaces;
using CodingTask.Application.Models;
using CodingTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CodingTask.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly CodingTaskContext _dbContext;

        private const string InvalidUserNameOrPassword = "Invalid Username or Password";

        public AuthService(
            ILogger<AuthService> logger,
            IConfiguration configuration,
            CodingTaskContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new AppException(InvalidUserNameOrPassword);
            }

            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new AppException(InvalidUserNameOrPassword);

            }

            var jwtToken = GenerateJwtToken(user.Id.ToString(), user.Username);

            return new AuthenticateResponseDto
            {
                AccessToken = jwtToken,
            };
        }


        private string GenerateJwtToken(string userId, string username)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim("name", username)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(securityToken);

            return jwtToken;
        }
    }

}
