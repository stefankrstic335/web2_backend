using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get")]
        //[Authorize(Roles = "customer")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpPost("post")]
        //[Authorize(Roles = "admin")]
        public IActionResult AddProduct(ProductDto productDto)
        {
            _productService.AddProduct(productDto);
            return Ok();
        }

        [HttpPost("delete")]
        //[Authorize(Roles = "admin")]
        public IActionResult RemoveProduct(ProductDto productDto)
        {
            _productService.RemoveProduct(productDto);
            return Ok();
        }
    }
}
