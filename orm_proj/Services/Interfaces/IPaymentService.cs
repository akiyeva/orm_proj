using orm_proj.Models;

namespace orm_proj.Services.Interfaces
{
    public interface IPaymentService
    {
        Task MakePaymentAsync(PaymentPostDto paymentDto);
        Task<List<PaymentGetDto>> GetAllPayments();
        Task<List<PaymentGetDto>> GetPaymentByUserId(int userId);
    }
}
