using orm_proj.Repositories.Interfaces.Generic;

namespace orm_proj.Repositories.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<OrderDetail>> GetByOrderIdAsync(int orderId);
    }
}
