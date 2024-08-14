using Microsoft.EntityFrameworkCore;
using orm_proj.Repositories.Implementations.Generic;
using orm_proj.Repositories.Interfaces;

namespace orm_proj.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
           return await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
