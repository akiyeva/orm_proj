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

            if (product == null)
                throw new NotFoundException("Product not found.");

            if (order == null)
                throw new NotFoundException("Order not found.");

            if (newOrderDetail.Quantity <= 0)
                throw new InvalidOrderDetailException("Quantity must be greater than zero.");

            if (product.Stock < newOrderDetail.Quantity)
                throw new InvalidOrderDetailException($"Insufficient stock for product {product.Name}.");

            product.Stock -= newOrderDetail.Quantity;
            _productRepository.Update(product);

            OrderDetail orderDetail = new OrderDetail()
            {
                ProductId = newOrderDetail.ProductId,
                OrderId = newOrderDetail.OrderId,
                Quantity = newOrderDetail.Quantity,
                PricePerItem = product.Price 
            };

            order.Details.Add(orderDetail);

            await _orderRepository.SaveChangesAsync();
            await _productRepository.SaveChangesAsync();
        }


        public async Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailRepository.GetFilterAsync(x => x.OrderId == orderId);

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
