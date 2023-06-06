using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Database.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MerchantId).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
