using CodingTask.Application.Exceptions;
using CodingTask.Application.Interfaces;
using CodingTask.Data;
using CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Application.Services
{
    public class CartService : ICartService
    {
        private readonly CodingTaskContext _context;
        private readonly IAuthService _authService;

        public CartService(CodingTaskContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems()
        {
            var userId = _authService.GetCurrentUserId();

            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();
        }

        public async Task AddToCart(int productId, int quantity)
        {
            var userId = _authService.GetCurrentUserId();

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product is not available");
            }

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            int newQuantity = (cartItem?.Quantity ?? 0) + quantity;

            if (newQuantity > product.Stock)
            {
                throw new AppException($"Product {product.Name} (#{product.Id}) is out of stock");
            }

            if (cartItem == null && quantity > 0)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else if (cartItem != null)
            {
                cartItem.Quantity = newQuantity;

                if (cartItem.Quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int productId)
        {
            var userId = _authService.GetCurrentUserId();

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item is not found");
            }
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task EmptyCart()
        {
            var userId = _authService.GetCurrentUserId();
            var cartItems = _context.CartItems.Where(x => x.UserId == userId);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }


    }
}
