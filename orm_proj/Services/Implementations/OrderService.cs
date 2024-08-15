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
        public OrderService(OrderRepository orderRepository,UserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task CreateOrderAsync(OrderPostDto newOrder)
        {
            var user = await _userRepository.GetSingleAsync(x => x.Id == newOrder.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            if (newOrder.TotalAmount <= 0)
            {
                throw new InvalidOrderException("Total amount must be greater than zero.");
            }

            Order order = new Order()
            {
                UserId = newOrder.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = newOrder.TotalAmount,
                Status = Enums.OrderStatus.Pending,
                Details = new List<OrderDetail>()
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();

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
            var orders = await _orderRepository.GetAllAsync("OrderStatus", "OrderDetail");

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
                    Details = new List<OrderDetail>()
                };
                result.Add(orderGet);
            });
            return result;
        }

        public async Task<List<OrderGetDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            List<OrderGetDto> result = new List<OrderGetDto>();

            orders.ForEach(order =>
            {
                OrderGetDto orderGet = new OrderGetDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status
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
