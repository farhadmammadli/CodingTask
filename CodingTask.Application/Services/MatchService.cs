using CodingTask.Application.Exceptions;
using CodingTask.Application.Interfaces;
using CodingTask.Data;
using CodingTask.Application;
using CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly CodingTaskContext _context;

        public MatchService(CodingTaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllMatchesAsync()
        {
            return await _context.Matches
                .Include(m => m.Submissions)
                .Include(m => m.WinnerUser)
                .ToListAsync();
        }

        public async Task<Match> GetCurrentMatchAsync()
        {
            var match = await _context.Matches
                .Where(m => m.ExpiryTimestamp > DateTime.Now)
                .OrderBy(m => m.ExpiryTimestamp)
                .FirstOrDefaultAsync();

            return match!;
        }

        public async Task<Match> CreateNewMatchAsync()
        {
            var match = await GetCurrentMatchAsync();
            if (match != null)
            {
                throw new AppException("There is already an active match");
            }

            match = new Match
            {
                ExpiryTimestamp = DateTime.Now.Add(Constants.MatchTimeout)
            };
            await _context.AddAsync(match);
            await _context.SaveChangesAsync();

            return match;
        }

        public async Task CalculateWinnerAsync(int matchId)
        {
            var match = await _context.Matches
                .Include(m => m.Submissions)
                .FirstOrDefaultAsync(m => m.Id == matchId);

            if (match == null) return;

            var winningSubmission = match.Submissions
                .OrderByDescending(s => s.GeneratedNumber)
                .FirstOrDefault();

            if (winningSubmission != null)
            {
                match.WinnerUserId = winningSubmission.UserId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
