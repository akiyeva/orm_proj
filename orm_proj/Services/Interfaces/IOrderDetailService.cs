using orm_proj.DTOs.OrderDetailDtos;

namespace orm_proj.Services.Interfaces
{
    public interface IOrderDetailService
    {
        Task AddOrderDetail(OrderDetailPostDto newOrderDetail);
        Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderId(int id);
    }
}
