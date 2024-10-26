using CodingTask.Application.Interfaces;
using CodingTask.Application.Mapping;
using CodingTask.Application.Models;
using CodingTask.Data;
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

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var list = await _context.Products.Select(x => x.ToProductDto()).ToListAsync();
            return list;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product is not found");
            }
            return product.ToProductDto();
        }
    }
}
