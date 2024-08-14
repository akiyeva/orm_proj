using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace orm_proj.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.UserName).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Password).IsRequired();
            builder.Property(a => a.Address).IsRequired();
        }
    }
}
