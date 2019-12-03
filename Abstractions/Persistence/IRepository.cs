using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.Abstractions.Persistence
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> QueryAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
