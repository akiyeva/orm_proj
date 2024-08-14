using orm_proj.Models;

namespace orm_proj.Services.Interfaces
{
    public interface IPaymentService
    {
        Task MakePaymentAsync(int orderId, decimal amount);
        Task<List<PaymentGetDto>> GetAllPayments(); 
                
    }
}
