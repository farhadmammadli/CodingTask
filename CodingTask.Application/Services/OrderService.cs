using CodingTask.Application.Exceptions;
using CodingTask.Application.Interfaces;
using CodingTask.Data;
using CodingTask.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly CodingTaskContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(CodingTaskContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Order> Checkout()
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]);

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
            {
                throw new AppException("Cart is empty.");
            }

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

            return order;
        }
    }
}
