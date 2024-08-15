using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace orm_proj.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(a => a.Quantity).IsRequired();
            builder.Property(a => a.PricePerItem).IsRequired().HasColumnType("decimal(6,2)");
        }
    }
}
