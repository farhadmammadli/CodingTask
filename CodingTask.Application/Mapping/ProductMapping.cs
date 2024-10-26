using CodingTask.Application.Models;
using CodingTask.Data.Models;

namespace CodingTask.Application.Mapping
{
    public static class ProductMapping
    {
        public static ProductDto ToProductDto(this Product model)
        {
            return new ProductDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                Images = model.ProductImages!.Select(x => x.ImagePath).ToList(),
            };
        }
    }
}
