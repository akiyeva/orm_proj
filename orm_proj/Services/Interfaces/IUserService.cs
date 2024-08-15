using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using orm_proj.Models;

namespace orm_proj.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterAdminAsync(UserPostDto newAdmin);
        Task RegisterUserAsync(UserPostDto newUser); 
        Task<UserGetDto> LoginUserAsync(string email, string password);  
        Task UpdateUserInfoAsync(int userId, UserPutDto user); 
        Task ExportUserOrdersToExcelAsync(int userId, string filePath);
        Task<User> GetUserByEmailAsync(string email);
    }
}
