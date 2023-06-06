﻿using AutoMapper;
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

        public void CompleteOrder(string orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order != null)
            {
                order.IsCompleted = true;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }

        public bool CreateOrder(OrderDto orderDto)
        {
            var products = _context.Products;

            var ord = _mapper.Map<Order>(orderDto);
            List<Product> productsList = new List<Product>(orderDto.OrderedProducts.Count);
            foreach (var prod in orderDto.OrderedProducts)
            {
                var old = products.FirstOrDefault(x => x.Id == prod.Id);
                productsList.Add(old);
            }

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

        public List<OrderDto> GetOrders(string email)
        {
            var orders = _context.Orders.Include(x => x.OrderedProducts).ToList();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            return _mapper.Map<List<OrderDto>>(orders.Where(x => x.OrderUserEmail == email).ToList());
        }
    }
}
