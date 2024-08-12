using orm_proj.Models;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class UserService : IUserService
    {
        public Task ExportUserOrdersToExcel(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetUserOrders(User user)
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserInfoAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
