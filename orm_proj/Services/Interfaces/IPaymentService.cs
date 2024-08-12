using orm_proj.Models;

namespace orm_proj.Services.Interfaces
{
    public interface IPaymentService
    {
        Task MakePaymentAsync();
        Task<List<Payment>> GetAllPayments();
                
    }
}
