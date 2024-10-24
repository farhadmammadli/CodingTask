using CodingTask.Data.Models;

namespace CodingTask.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> Checkout();
    }
}
