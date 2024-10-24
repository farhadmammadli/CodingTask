using CodingTask.Application.Interfaces;
using CodingTask.Data.Models;
using CodingTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CodingTask.Application.Exceptions;
using CodingTask.Application.Models;

namespace CodingTask.Application.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly CodingTaskContext _context;
        private readonly IMatchService _matchService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubmissionService(CodingTaskContext context, IMatchService matchService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _matchService = matchService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SubmissionDto> SubmitNumberAsync()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userId = currentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var currentMatch = await _matchService.GetCurrentMatchAsync();

            if (currentMatch == null)
            {
                throw new AppException("There is no active match. Please wait.");
            }

            var existingSubmission = await _context.Submissions
                .AnyAsync(s => s.UserId == int.Parse(userId) && s.MatchId == currentMatch.Id);

            if (existingSubmission)
            {
                throw new AppException("You have already submitted for this match");
            }

            var random = new Random();
            var generatedNumber = random.Next(0, 101);

            var submission = new Submission
            {
                UserId = int.Parse(userId),
                MatchId = currentMatch.Id,
                GeneratedNumber = generatedNumber,
                Timestamp = DateTime.Now
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return new SubmissionDto
            {
                GeneratedNumber = generatedNumber,
                Match = currentMatch
            };
        }
    }

}
