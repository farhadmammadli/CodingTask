namespace CodingTask.Application.Models
{
    public class AuthenticateResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
