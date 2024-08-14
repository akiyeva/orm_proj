using Microsoft.EntityFrameworkCore;
using orm_proj.Utils;

namespace orm_proj.Contexts
{
    public class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
