using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orm_proj.Enums;

namespace orm_proj.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(a => a.TotalAmount).IsRequired();
            builder.Property(a => a.Status).HasDefaultValue(OrderStatus.Pending);
        }
    }
}
