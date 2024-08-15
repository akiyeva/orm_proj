using orm_proj.Enums;
using orm_proj.Models;
using orm_proj.Repositories.Implementations;
using orm_proj.Repositories.Interfaces;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        public PaymentService(PaymentRepository paymentRepository, OrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }
        public async Task MakePaymentAsync(PaymentPostDto paymentDto)
        {
            if (paymentDto.Amount <= 0)
            {
                throw new InvalidPaymentException("Payment amount must be greater than zero.");
            }

            var order = await _orderRepository.GetSingleAsync(x => x.Id == paymentDto.OrderId);
            if (order == null)
            {
                throw new NotFoundException($"Order with ID {paymentDto.OrderId} not found.");
            }

            if (order.Status == OrderStatus.Cancelled || order.Status == OrderStatus.Completed)
            {
                throw new InvalidPaymentException("You can only make payment for pending order.");
            }

            Payment payment = new Payment
            {
                OrderId = paymentDto.OrderId,
                Amount = paymentDto.Amount,
                PaymentDate = paymentDto.PaymentDate
            };

            await _paymentRepository.CreateAsync(payment);
            order.Status = OrderStatus.Completed;

            _orderRepository.Update(order);

            await _orderRepository.SaveChangesAsync();
            await _paymentRepository.SaveChangesAsync();

        }

        public async Task<List<PaymentGetDto>> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllAsync();

            List<PaymentGetDto> result = new List<PaymentGetDto>();

            payments.ForEach(payment =>
            {
                PaymentGetDto paymentGet = new PaymentGetDto()
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate
                };
                result.Add(paymentGet);
            });
            return result;
        }

        public async Task<List<PaymentGetDto>> GetPaymentByUserId(int userId)
        {
            var payments = await _paymentRepository.GetFilterAsync(x => x.Order.UserId == userId);

            List<PaymentGetDto> result = new List<PaymentGetDto>();

            payments.ForEach(payment =>
            {
                PaymentGetDto paymentGet = new PaymentGetDto()
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate
                };
                result.Add(paymentGet);
            });
            return result;
        }
    }
}
