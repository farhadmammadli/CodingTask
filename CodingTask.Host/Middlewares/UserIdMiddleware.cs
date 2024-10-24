using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using CodingTask.Data;

namespace CodingTask.Host.Middlewares
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CodingTaskContext dbContext)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "User1");

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.Id.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "Custom");
                context.User = new ClaimsPrincipal(userIdentity);
                context.Items["UserId"] = user.Id;
            }

            await _next(context);
        }
    }
}
