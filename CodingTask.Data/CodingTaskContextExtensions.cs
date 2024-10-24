using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTask.Data
{
    public static class CodingTaskContextExtensions
    {
        public static async Task Initialize(this CodingTaskContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var currentUsers = await context.Users.ToListAsync();

            bool anyNewUser = false;

            if (!currentUsers.Any(u => u.Username == "User1"))
            {
                context.Users.Add(new Models.User
                {
                    Username = "User1",
                    Password = ""
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.Username == "User2"))
            {
                context.Users.Add(new Models.User
                {
                    Username = "User2",
                    Password = ""
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.Username == "User3"))
            {
                context.Users.Add(new Models.User
                {
                    Username = "User3",
                    Password = ""
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.Username == "User4"))
            {
                context.Users.Add(new Models.User
                {
                    Username = "User4",
                    Password = ""
                });

                anyNewUser = true;
            }

            if (anyNewUser)
            {
                await context.SaveChangesAsync(); 
            }
        }
    }
}
