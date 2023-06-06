using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string MerchantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; } 

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public List<Order> Orders { get; set; }


        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
