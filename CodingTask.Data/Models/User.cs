﻿using System.Collections.Generic;

namespace CodingTask.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
