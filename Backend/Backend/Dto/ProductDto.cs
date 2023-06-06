using Backend.Models;

namespace Backend.Dto
{
    public class ProductDto
    {
        public string Id { get; set; }

        public string MerchantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
