using CodingTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMatches()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentMatch()
        {
            var match = await _matchService.GetCurrentMatchAsync();
            return Ok(match);
        }
    }
}
