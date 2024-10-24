using CodingTask.Data.Models;

namespace CodingTask.Application.Interfaces
{
    public interface IMatchService
    {
        Task<IEnumerable<Match>> GetAllMatchesAsync();
        Task<Match> GetCurrentMatchAsync();
        Task<Match> CreateNewMatchAsync();
        Task CalculateWinnerAsync(int matchId);
    }
}
