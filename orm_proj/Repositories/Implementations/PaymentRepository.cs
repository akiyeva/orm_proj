using orm_proj.Repositories.Implementations.Generic;
using orm_proj.Repositories.Interfaces;

namespace orm_proj.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly AppDBContext _context;
        public PaymentRepository(AppDBContext context)
        {
            _context = context;
        }
    }
}
