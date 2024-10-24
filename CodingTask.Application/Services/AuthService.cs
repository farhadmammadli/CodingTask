using CodingTask.Application.Exceptions;
using CodingTask.Application.Interfaces;
using CodingTask.Application.Models;
using CodingTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodingTask.Data.Models;

namespace CodingTask.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashService _hashService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly CodingTaskContext _dbContext;

        private const string InvalidUserNameOrPassword = "Invalid Username or Password";
        private const string InternalAuthorizationError = "An internal authorization error occured. Please contact administrator.";

        public AuthService(
            IHashService hashService,
            ILogger<AuthService> logger,
            IConfiguration configuration,
            CodingTaskContext dbContext)
        {
            _hashService = hashService;
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

            User user;
            var hashPassword = await _hashService.HashText(model.Password);

            try
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == model.Username && u.Password == hashPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occured when querying user");
                throw new AppException(InternalAuthorizationError);
            }

            if (user == null)
            {
                throw new AppException(InvalidUserNameOrPassword);
            }

            var jwtToken = GenerateJwtToken(user.Id.ToString(), user.UserName);

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
