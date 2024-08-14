using orm_proj.Repositories.Interfaces.Generic;
using System.Linq.Expressions;

namespace orm_proj.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> SearchAsync(Expression<Func<Product, bool>> predicate);
     
    }
}
