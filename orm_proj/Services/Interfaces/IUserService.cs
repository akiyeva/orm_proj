using orm_proj.Models;

namespace orm_proj.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task LoginAsync(User user);
        Task UpdateUserInfoAsync(User user);
        Task<List<Order>> GetUserOrders(User user); 
        Task ExportUserOrdersToExcel(User user);
    }
}
