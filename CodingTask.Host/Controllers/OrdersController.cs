using CodingTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CodingTask.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Host.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CodingTaskContext _context;

        public OrdersController(CodingTaskContext context)
        {
            _context = context;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any()) return BadRequest("Cart is empty.");

            var order = new Order { UserId = userId, OrderDate = DateTime.Now };
            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem { ProductId = item.ProductId, Quantity = item.Quantity });
                item.Product.Stock -= item.Quantity;
            }

            order.TotalAmount = order.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity);

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }

}
