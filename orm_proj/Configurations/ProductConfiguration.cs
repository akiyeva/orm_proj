using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace orm_proj.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(a => a.Name).IsUnique();
            builder.Property(a => a.Description).IsRequired().HasMaxLength(255);
            builder.Property(a => a.Stock).IsRequired();
            builder.Property(a => a.Price).IsRequired().HasColumnType("decimal(6,2)");
        }
    }
}
