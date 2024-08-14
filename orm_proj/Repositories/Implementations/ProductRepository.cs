using Microsoft.EntityFrameworkCore;
using orm_proj.Repositories.Implementations.Generic;
using orm_proj.Repositories.Interfaces;
using System.Linq.Expressions;

namespace orm_proj.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDBContext _context;
        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.Where(predicate).ToListAsync();
        }
    }
}
