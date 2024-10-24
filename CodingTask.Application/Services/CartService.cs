using CodingTask.Application.Interfaces;
using CodingTask.Data;

namespace CodingTask.Application.Services
{
    public class CartService : ICartService
    {
        private readonly CodingTaskContext _context;

        public CartService(CodingTaskContext context)
        {
            _context = context;
        }


    }
}
