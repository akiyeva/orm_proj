using Microsoft.EntityFrameworkCore;
using orm_proj.Repositories.Implementations.Generic;
using orm_proj.Repositories.Interfaces;

namespace orm_proj.Repositories.Implementations
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppDBContext _context;
        public OrderDetailRepository(AppDBContext context)
        {
            _context = context;
        }
    }
}
