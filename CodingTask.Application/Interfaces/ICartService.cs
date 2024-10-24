using CodingTask.Data.Models;

namespace CodingTask.Application.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItems();
        Task AddToCart(int productId, int quantity);
        Task RemoveFromCart(int productId);
        Task EmptyCart();
    }
}
