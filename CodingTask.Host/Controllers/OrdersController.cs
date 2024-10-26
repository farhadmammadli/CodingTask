﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CodingTask.Application.Interfaces;

namespace CodingTask.Host.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var result = await _service.Checkout();
            return Ok(result);
        }
    }

}
