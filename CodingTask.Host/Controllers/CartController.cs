using CodingTask.Application.Exceptions;
using CodingTask.Data;
using CodingTask.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTask.Host.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly CodingTaskContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartController(CodingTaskContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]);

            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromForm] int productId, [FromForm] int quantity)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]);
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product is not available");
            }
            if (product.Stock < quantity)
            {
                throw new AppException($"Product {product.Name} (#{product.Id}) is not available");
            }

            var cartItem = await _context.CartItems
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            if (cartItem.Quantity <= 0)
            {
                _context.Remove(cartItem);
            }

            if (cartItem.Quantity > product.Stock)
            {
                throw new AppException($"Product {product.Name} (#{product.Id}) is out of stock");
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]);

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item is not found");
            }
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> EmptyCart()
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]);
            var cartItems = _context.CartItems.Where(x => x.UserId == userId);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}