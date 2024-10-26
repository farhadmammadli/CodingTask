using CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;
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

            if (!currentUsers.Exists(u => u.Username == "User1"))
            {
                context.Users.Add(new User
                {
                    Username = "User1",
                    Password = "Password1"
                });
            }


            // Product seeding logic
            var currentProducts = await context.Products.ToListAsync();

            if (!currentProducts.Exists(p => p.Name == "Laptop"))
            {
                context.Products.Add(new Product
                {
                    Name = "Laptop",
                    Description = "A high-performance laptop",
                    Price = 999.99m,
                    Stock = 10,
                    ProductImages = {
                        new ProductImage { ImagePath = "/img/76e0e955-7d69-4045-8dad-2bcd7ce23e56.jpg" },
                        new ProductImage { ImagePath = "/img/76e0e955-7d69-4045-8dad-2bcd7ce23e56.jpg" },
                        new ProductImage { ImagePath = "/img/76e0e955-7d69-4045-8dad-2bcd7ce23e56.jpg" }
                    }
                });
            }

            if (!currentProducts.Exists(p => p.Name == "Smartphone"))
            {
                context.Products.Add(new Product
                {
                    Name = "Smartphone",
                    Description = "A new-gen smartphone",
                    Price = 699.99m,
                    Stock = 20,
                    ProductImages = {
                        new ProductImage { ImagePath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8" },
                        new ProductImage { ImagePath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8" },
                        new ProductImage { ImagePath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8" }
                    }
                });
            }

            if (!currentProducts.Exists(p => p.Name == "Headphones"))
            {
                context.Products.Add(new Product
                {
                    Name = "Headphones",
                    Description = "Noise-cancelling headphones",
                    Price = 199.99m,
                    Stock = 30,
                    ProductImages = {
                        new ProductImage { ImagePath = "/img/2cb4c65a-b331-44a0-b896-d0cde08ca3dd" },
                        new ProductImage { ImagePath = "/img/2cb4c65a-b331-44a0-b896-d0cde08ca3dd" },
                        new ProductImage { ImagePath = "/img/2cb4c65a-b331-44a0-b896-d0cde08ca3dd" }
                    }
                });
            }

            if (!currentProducts.Exists(p => p.Name == "Monitor"))
            {
                context.Products.Add(new Product
                {
                    Name = "Monitor",
                    Description = "4K Ultra HD Monitor",
                    Price = 299.99m,
                    Stock = 15,
                    ProductImages = {
                        new ProductImage { ImagePath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8" },
                        new ProductImage { ImagePath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8" },
                        new ProductImage { ImagePath = "/img/0deb0480-9e0e-43a7-9a13-eae2650908c8" }
                    }
                });
            }

            await context.SaveChangesAsync();


        }
    }
}
