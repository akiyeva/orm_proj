using Microsoft.EntityFrameworkCore;
using orm_proj.Contexts;
using orm_proj.Models.Common;
using orm_proj.Repositories.Interfaces.Generic;
using System.Linq.Expressions;

namespace orm_proj.Repositories.Implementations.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly AppDBContext _context;
        public Repository()
        {
            _context = new AppDBContext();
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
          _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _context.Set<T>().Where(expression);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = await query.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().AnyAsync(predicate);
            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
