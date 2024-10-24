using CodingTask.Application.Models;

namespace CodingTask.Application.Interfaces
{
    public interface ISubmissionService
    {
        Task<SubmissionDto> SubmitNumberAsync();
    }
}
