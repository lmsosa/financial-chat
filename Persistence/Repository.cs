using FinancialChat.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.Persistence
{
    public class Repository<TDbContext, T> : IRepository<T> where TDbContext : DbContext where T : class
    {
        protected TDbContext _dbContext;

        public Repository(TDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<T> QueryAsync()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
