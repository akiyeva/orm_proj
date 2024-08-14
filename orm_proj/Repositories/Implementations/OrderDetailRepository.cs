using Microsoft.EntityFrameworkCore;
using orm_proj.Repositories.Implementations.Generic;
using orm_proj.Repositories.Interfaces;

namespace orm_proj.Repositories.Implementations
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppDBContext _context;
        public OrderDetailRepository()
        {
            _context = new AppDBContext();
        }
        public async Task<List<OrderDetail>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
        }
    }
}
