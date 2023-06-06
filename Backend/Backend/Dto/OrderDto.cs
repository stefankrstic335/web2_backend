using Backend.Models;
using System.Collections.Generic;

namespace Backend.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string OrderUserEmail { get; set; }

        public string ShopperAddress { get; set; }

        public List<Product> OrderedProducts { get; set; }
    }
}
