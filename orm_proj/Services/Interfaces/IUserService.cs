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
        Task<User> GetUserByEmailAsync(string email);
        Task<List<UserGetDto>> GetAllUsers();
        //bool CheckUsername(string name);
        //bool CheckPassword(string password);
        //bool CheckEmail(string email);
    }
}
