using CodingTask.Application.Interfaces;
using CodingTask.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult<AuthenticateResponseDto>> Authenticate(AuthenticateRequestDto request)
        {
            var result = await _authService.Authenticate(request);
            return result;
        }
    }
}
