using System;
using System.Collections.Generic;

namespace Backend.Models
{

    public enum OrderStatus
    {
        Canceled,
        InProgress,
        Completed
    }

    public class Order
    {
        public string Id { get; set; }

        public string OrderUserEmail { get; set; }

        public string ShopperAddress { get; set; }

        public List<Product> OrderedProducts { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int DeliveryPrice { get; set; }
            
        public OrderStatus OrderStatus { get; set; }
        
        public Order()
        {
            Id = Guid.NewGuid().ToString();
            DeliveryPrice = 250;
        }

    }
}
