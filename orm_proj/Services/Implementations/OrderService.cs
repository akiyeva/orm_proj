using orm_proj.Enums;
using orm_proj.Repositories.Implementations;
using orm_proj.Repositories.Interfaces;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(OrderRepository orderRepository,UserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(OrderPostDto newOrder)
        {
            var user = await _userRepository.GetSingleAsync(x => x.Id == newOrder.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            if (newOrder.Details == null || !newOrder.Details.Any())
            {
                throw new InvalidOrderException("Order must contain at least one product.");
            }

            Order order = new Order()
            {
                UserId = newOrder.UserId,
                OrderDate = DateTime.UtcNow,
                Status = Enums.OrderStatus.Pending,
                Details = new List<OrderDetail>()
            };

            decimal totalAmount = 0;

            foreach (var detail in newOrder.Details)
            {
                var product = await _productRepository.GetSingleAsync(x => x.Id == detail.ProductId);

                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {detail.ProductId} not found.");
                }

                if (product.Stock < detail.Quantity)
                {
                    throw new InvalidOrderException($"Product {product.Name} is out of stock or insufficient quantity available.");
                }

                product.Stock -= detail.Quantity;

                _productRepository.Update(product);

                decimal productTotalPrice = product.Price * detail.Quantity;
                totalAmount += productTotalPrice;

                order.Details.Add(new OrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity
                });
            }

            order.TotalAmount = totalAmount;

            await _orderRepository.CreateAsync(order);

            await _orderRepository.SaveChangesAsync();
            await _productRepository.SaveChangesAsync();
        }


        public async Task CancelOrderAsync(int id)
        {
            var order = await _getOrderById(id);

            order.Status = Enums.OrderStatus.Cancelled;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CompleteOrderAsync(int id)
        {
            var order = await _getOrderById(id);

            order.Status = Enums.OrderStatus.Completed;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<OrderGetDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            List<OrderGetDto> result = new List<OrderGetDto>();

            orders.ForEach(order =>
            {
                OrderGetDto orderGet = new OrderGetDto()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = order.TotalAmount,
                    Status = Enums.OrderStatus.Pending,
                    Details = order.Details
                };
                result.Add(orderGet);
            });
            return result;
        }

        public async Task<List<OrderGetDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetFilterAsync(x=>x.UserId==userId);

            List<OrderGetDto> result = new List<OrderGetDto>();

            orders.ForEach(order =>
            {
                OrderGetDto orderGet = new OrderGetDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,
                    Details = order.Details
                };
                result.Add(orderGet);
            });
            return result;
        }

        public async Task<Order> _getOrderById(int id)
        {
            var order = await _orderRepository.GetSingleAsync(x => x.Id == id);

            if (order == null)
                throw new NotFoundException("Order not found");

            return order;
        }

    }
}
