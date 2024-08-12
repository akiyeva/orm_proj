using orm_proj.Models;

namespace orm_proj.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task CancelOrderAsync(Order order);
        Task CompleteOrderAsync(Order order);
        Task AddOrderDetailAsync(Order order);
        Task<List<Order>> GetAllOrders();
        Task<List<OrderDetail>> GetOrderDetailByOrderIdAsync();
    }
}
