
namespace CodingTask.Application.Models
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
}
