using CodingTask.Application.Models;

namespace CodingTask.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model);
    }
}
