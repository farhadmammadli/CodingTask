using CodingTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpPost("play")]
        [Authorize]
        public async Task<IActionResult> Play()
        {
            var result = await _submissionService.SubmitNumberAsync();
            return Ok(result);
        }
    }
}
