using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Dto
{
    public class OrderDto
    {
        public string Id { get; set; }

        public string OrderUserEmail { get; set; }

        public string ShopperAddress { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }

        public List<ProductDto> OrderedProducts { get; set; }
    }
}
