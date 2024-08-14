using orm_proj.DTOs.OrderDetailDtos;
using orm_proj.Repositories.Implementations;
using orm_proj.Repositories.Interfaces;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderDetailService(OrderDetailRepository orderDetailRepository, ProductRepository productRepository, OrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        public async Task AddOrderDetail(OrderDetailPostDto newOrderDetail)
        {
            var product = await _productRepository.GetSingleAsync(x => x.Id == newOrderDetail.ProductId);
            var order = await _orderRepository.GetSingleAsync(x => x.Id == newOrderDetail.OrderId);

            if (product == null || order == null)
                throw new NotFoundException();

            if (newOrderDetail.Quantity < 0 || newOrderDetail.PricePerItem < 0)
                throw new InvalidOrderDetailException();

            OrderDetail orderDetail = new OrderDetail()
            {
                ProductId = newOrderDetail.ProductId,
                OrderId = newOrderDetail.OrderId,
                Quantity = newOrderDetail.Quantity,
                PricePerItem = newOrderDetail.PricePerItem,
            };
        }

        public async Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderId(int id)
        {
            var orderDetails = await _orderDetailRepository.GetByOrderIdAsync(id);

            List<OrderDetailGetDto> result = new List<OrderDetailGetDto>();

            orderDetails.ForEach(orderDetail =>
            {
                OrderDetailGetDto orderDetailGet = new OrderDetailGetDto()
                {
                    Id = orderDetail.Id,
                    ProductId = orderDetail.ProductId,
                    OrderId = orderDetail.OrderId,
                    Quantity = orderDetail.Quantity,
                    PricePerItem = orderDetail.PricePerItem,
                };
                result.Add(orderDetailGet);
            });
            return result;
        }
    }
}
