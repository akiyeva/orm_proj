using orm_proj.Models;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        public Task<List<Payment>> GetAllPayments()
        {
            throw new NotImplementedException();
        }

        public Task MakePaymentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
