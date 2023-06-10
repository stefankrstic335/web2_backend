using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDto productDto);
        void RemoveProduct(ProductDto productDto);
        List<ProductDto> GetProducts();
        List<ProductDto> GetProductsForMerchant(string merchantId);

    }
}
