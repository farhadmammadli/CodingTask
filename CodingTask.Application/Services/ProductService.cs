using CodingTask.Application.Interfaces;
using CodingTask.Data;
using CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly CodingTaskContext _context;

        public ProductService(CodingTaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product is not found");
            }
            return product;
        }
    }
}
