namespace orm_proj.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderPostDto newOrder);  //create
        Task CancelOrderAsync(int id);  //update status
        Task CompleteOrderAsync(int id);  //update status
        Task<List<OrderGetDto>> GetAllOrders();
        Task<List<OrderGetDto>> GetUserOrdersAsync(int userId);
       // Task<List<OrderGetDto>> GetPendingOrdersByUserIdAsync(int userId);
    }
}
