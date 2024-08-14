using orm_proj.Repositories.Interfaces.Generic;

namespace orm_proj.Repositories.Interfaces
{
    public interface IUserRepository :IRepository<User> 
    {
        Task<User> GetByEmailAsync(string email);
    }
}
