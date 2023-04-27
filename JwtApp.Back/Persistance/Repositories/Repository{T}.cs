
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JwtApp.Back.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly JwtContext _jwtContext;

        public Repository(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _jwtContext.Set<T>().AddAsync(entity);
            await _jwtContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _jwtContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _jwtContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _jwtContext.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _jwtContext.Set<T>().Remove(entity);
            await _jwtContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _jwtContext.Set<T>().Update(entity);
            await _jwtContext.SaveChangesAsync();
        }
    }
}
