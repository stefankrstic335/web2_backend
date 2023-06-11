using AutoMapper;
using Backend.Database;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly LocalDbContext _context;

        public OrderService(IMapper mapper, LocalDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void CancelOrder(string orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Canceled;

                foreach(Product product in order.OrderedProducts)
                {
                    _context.Products.FirstOrDefault(x => x.Id == product.Id).Quantity += 1;
                }
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }

        public void CompleteOrder(string orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Completed;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }

        public bool CreateOrder(OrderDto orderDto)
        {
            var products = _context.Products;

            var ord = _mapper.Map<Order>(orderDto);
            ord.Id = Guid.NewGuid().ToString();
            List<Product> productsList = new List<Product>(orderDto.OrderedProducts.Count);
            foreach (var prod in orderDto.OrderedProducts)
            {
                var old = products.FirstOrDefault(x => x.Id == prod.Id);
                old.Quantity -= 1;
                productsList.Add(old);
            }

            ord.OrderStatus = OrderStatus.InProgress;

            ord.OrderedProducts = productsList;

            var seed = 3;
            var random = new Random(seed);

            var rNum = random.Next(48, 120);


            ord.StartTime = DateTime.Now;


            ord.EndTime = DateTime.Now.AddMinutes((double)(rNum));

            _context.Orders.Add(ord);
            _context.SaveChanges();
            return true;
        }

        public List<OrderDto> GetAllOrders()
        {
            return _mapper.Map<List<OrderDto>>(_context.Orders).ToList();
        }

        public List<OrderDto> GetCanceledOrdersShopper(string email)
        {
            var orders = _context.Orders.Include(x => x.OrderedProducts).ToList();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            return _mapper.Map<List<OrderDto>>(orders.Where(x => x.OrderUserEmail == email && x.OrderStatus == OrderStatus.Canceled).ToList());
        }
        public List<OrderDto> GetNonCanceledOrdersShopper(string email)
        {
            var orders = _context.Orders.Include(x => x.OrderedProducts).ToList();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            return _mapper.Map<List<OrderDto>>(orders.Where(x => x.OrderUserEmail == email && x.OrderStatus != OrderStatus.Canceled).ToList());
        }

        public List<OrderDto> GetNewOrdersMerchant(string email)
        {
            var orders = _context.Orders.Include(x => x.OrderedProducts).ToList();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            List<OrderDto> retList = new List<OrderDto>(0);
            orders = orders.Where(x=> x.OrderStatus == OrderStatus.InProgress).ToList();
            foreach(var order in orders)
            {
                foreach(var product in order.OrderedProducts)
                {
                    if(product.MerchantId == email)
                    {
                        retList.Add(_mapper.Map<OrderDto>(order));
                    }
                }
            }

            return retList;
        }
        public List<OrderDto> GetAllOrdersMerchant(string email)
        {
            var orders = _context.Orders.Include(x => x.OrderedProducts).ToList();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            List<OrderDto> retList = new List<OrderDto>(0);
            orders = orders.Where(x => x.OrderStatus == OrderStatus.Completed).ToList();
            foreach (var order in orders)
            {
                foreach (var product in order.OrderedProducts)
                {
                    if (product.MerchantId == email)
                    {
                        retList.Add(_mapper.Map<OrderDto>(order));
                    }
                }
            }

            return retList;
        }

    }
}
