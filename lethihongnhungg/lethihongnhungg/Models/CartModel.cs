﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lethihongnhungg.DB;

namespace lethihongnhungg.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Sale_price { get; set; }
    }
}