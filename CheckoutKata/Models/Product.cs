﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Models
{
    public class Product
    {
        public string sku { get; set; }
        public float price { get; set; }

        public Product(string _sku, float _price)
        {
            sku = _sku;
            price = _price;
        }
    }
}
