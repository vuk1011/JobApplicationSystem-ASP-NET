using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(params object[] keyValues);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
