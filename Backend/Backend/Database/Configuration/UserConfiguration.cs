using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(32);
            builder.Property(x => x.Username).HasMaxLength(32);
            builder.Property(x => x.Lastname).HasMaxLength(32);
            builder.Property(x => x.AccountType).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(255);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.AccountVerified).IsRequired();
            builder.Property(x => x.Address).IsRequired();

        }
    }
}
