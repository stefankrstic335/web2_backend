using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getAllOrders")]
        //[Authorize(Roles = "deliverer")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }


        [HttpGet("getOrders")]
        //[Authorize(Roles = "deliverer")]
        public IActionResult GetOrders(string email)
        {
            return Ok(_orderService.GetOrders(email));
        }

        [HttpPost("createOrder")]
        //[Authorize(Roles = "customer")]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            return Ok(_orderService.CreateOrder(orderDto));
        }

        [HttpPost("completeOrder")]
        //[Authorize(Roles = "deliverer")]
        public IActionResult DeliverOrder(string orderId)
        {
            _orderService.CompleteOrder(orderId);
            return Ok();
        }

    }
}
