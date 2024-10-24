using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodingTask.Host.Middlewares
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Example: Extracting the user ID from JWT claims (if using JWT authentication)
            if (context.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                // Get the user ID claim (assuming the claim type is "UserId")
                var userIdClaim = identity.Claims.FirstOrDefault(c => c.Type == "UserId");

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    // Set the user ID in the HttpContext.Items collection
                    context.Items["UserId"] = userId;
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
