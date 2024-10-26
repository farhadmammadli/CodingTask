using CodingTask.Application.Models;

namespace CodingTask.Application.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItems();
        Task AddToCart(int productId, int quantity);
        Task RemoveFromCart(int productId);
        Task EmptyCart();
    }
}
