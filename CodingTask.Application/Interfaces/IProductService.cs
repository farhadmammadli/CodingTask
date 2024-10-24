using CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
    }
}
