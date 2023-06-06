using AutoMapper;
using Backend.Database;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using System.Collections.Generic;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly LocalDbContext _context;

        public ProductService(IMapper mapper, LocalDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddProduct(ProductDto productDto)
        {
            var prod = _mapper.Map<Product>(productDto);
            _context.Products.Add(prod);
            _context.SaveChanges();
        }

        public List<ProductDto> GetProducts()
        {
            return _mapper.Map<List<ProductDto>>(_context.Products);
        }

        public void RemoveProduct(ProductDto productDto)
        {
            _context.Products.Remove(_mapper.Map<Product>(productDto));
            _context.SaveChanges();
        }
    }
}
