using CodingTask.Application.Models;

namespace CodingTask.Application.Interfaces
{
    public interface IAuthService
    {
        int GetCurrentUserId();
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model);
    }
}
