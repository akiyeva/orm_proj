using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace orm_proj.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a => a.Name).IsRequired(true).HasMaxLength(100);
            builder.HasIndex(a => a.Name).IsUnique();
        }
    }
}
