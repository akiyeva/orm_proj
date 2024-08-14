using Microsoft.EntityFrameworkCore;
using orm_proj.Repositories.Implementations.Generic;
using orm_proj.Repositories.Interfaces;

namespace orm_proj.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
