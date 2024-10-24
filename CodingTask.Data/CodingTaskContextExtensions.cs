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



            // Product seeding logic
            var currentProducts = await context.Products.ToListAsync();
            bool anyNewProduct = false;

            if (!currentProducts.Any(p => p.Name == "Laptop"))
            {
                context.Products.Add(new Models.Product
                {
                    Name = "Laptop",
                    Description = "A high-performance laptop",
                    Price = 999.99m,
                    Stock = 10,
                    ImgPath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8.jpg"

                });
                anyNewProduct = true;
            }

            if (!currentProducts.Any(p => p.Name == "Smartphone"))
            {
                context.Products.Add(new Models.Product
                {
                    Name = "Smartphone",
                    Description = "A new-gen smartphone",
                    Price = 699.99m,
                    Stock = 20,
                    ImgPath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8.jpg"
                });
                anyNewProduct = true;
            }

            if (!currentProducts.Any(p => p.Name == "Headphones"))
            {
                context.Products.Add(new Models.Product
                {
                    Name = "Headphones",
                    Description = "Noise-cancelling headphones",
                    Price = 199.99m,
                    Stock = 30,
                    ImgPath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8.jpg"
                });
                anyNewProduct = true;
            }

            if (!currentProducts.Any(p => p.Name == "Monitor"))
            {
                context.Products.Add(new Models.Product
                {
                    Name = "Monitor",
                    Description = "4K Ultra HD Monitor",
                    Price = 299.99m,
                    Stock = 15,
                    ImgPath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8.jpg"
                });
                anyNewProduct = true;
            }

            if (anyNewProduct)
            {
                await context.SaveChangesAsync();
            }


        }
    }
}
