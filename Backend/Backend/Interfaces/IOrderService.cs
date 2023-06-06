using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders(string email);
        List<OrderDto> GetAllOrders();
        bool CreateOrder(OrderDto orderDto);
        void CompleteOrder(string orderId);



    }
}
