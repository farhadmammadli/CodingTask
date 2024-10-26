using CodingTask.Application.Models;
using CodingTask.Data.Models;

namespace CodingTask.Application.Mapping
{
    public static class CartMapping
    {
        public static CartItemDto ToCartItemDto(this CartItem model)
        {
            return new CartItemDto
            {
                Id = model.Id,
                ProductId = model.ProductId,
                Product = model.Product.ToProductDto(),
                UserId = model.UserId,
                Quantity = model.Quantity,
            };
        }
    }
}
