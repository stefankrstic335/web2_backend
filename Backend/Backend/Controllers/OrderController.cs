using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getAllOrders")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }


        [HttpGet("getNonCanceledOrders")]
        [Authorize(Roles = "shopper")]
        public IActionResult GetNonCanceledOrders(string email)
        {
            return Ok(_orderService.GetNonCanceledOrdersShopper(email));
        }
        [HttpGet("getCanceledOrders")]
        [Authorize(Roles = "shopper")]
        public IActionResult GetCanceledOrders(string email)
        {
            return Ok(_orderService.GetCanceledOrdersShopper(email));
        }

        [HttpPost("createOrder")]
        [Authorize(Roles = "shopper")]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            try
            {
                bool res = _orderService.CreateOrder(orderDto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("completeOrder")]
        [Authorize(Roles = "merchant")]
        public IActionResult DeliverOrder(string orderId)
        {
            _orderService.CompleteOrder(orderId);
            return Ok();
        }

    }
}
