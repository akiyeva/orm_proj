using orm_proj.Models;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public Task AddOrderDetailAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task CancelOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task CompleteOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDetail>> GetOrderDetailByOrderIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
