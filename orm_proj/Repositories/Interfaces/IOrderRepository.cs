﻿using orm_proj.Repositories.Interfaces.Generic;

namespace orm_proj.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
