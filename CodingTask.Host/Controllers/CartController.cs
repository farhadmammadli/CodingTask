using CodingTask.Application.Interfaces;
using CodingTask.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingTask.Host.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems()
        {
            var result = await _service.GetCartItems();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromForm] int productId, [FromForm] int quantity)
        {
            await _service.AddToCart(productId, quantity);
            return Ok();
        }

        [HttpDelete("product/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _service.RemoveFromCart(productId);
            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> EmptyCart()
        {
            await _service.EmptyCart();
            return Ok();
        }
    }
}