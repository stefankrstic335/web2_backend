using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get")]
        [Authorize(Roles = "shopper")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet("getProductsForMerchant")]
        [Authorize(Roles = "merchant")]
        public IActionResult GetProductsForMerchant(string merchantId)
        {
            return Ok(_productService.GetProductsForMerchant(merchantId));
        }

        [HttpPost("post")]
        [Authorize(Roles = "merchant")]
        public IActionResult AddProduct(ProductDto productDto)
        {
            try
            {
                _productService.AddProduct(productDto);
                return NoContent();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("delete")]
        [Authorize(Roles = "merchant")]
        public IActionResult RemoveProduct(ProductDto productDto)
        {
            _productService.RemoveProduct(productDto);
            return Ok();
        }
    }
}
