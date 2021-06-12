using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Worktop.Core.Domain.Data
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> Get(int id);
        Task<T> Find(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Fetch();
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}