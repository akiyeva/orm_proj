using orm_proj.Models.Common;
using System.Linq.Expressions;

namespace orm_proj.Repositories.Interfaces.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync(params string[] includes);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
