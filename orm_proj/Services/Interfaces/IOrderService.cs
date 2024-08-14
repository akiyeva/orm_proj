namespace orm_proj.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderPostDto newOrder);  //create
        Task CancelOrderAsync(OrderPutDto newOrder);  //update status
        Task CompleteOrderAsync(OrderPutDto newOrder);  //update status
        Task<List<OrderGetDto>> GetAllOrders();
        Task<List<OrderGetDto>> GetUserOrdersAsync(int userId);
    }
}
