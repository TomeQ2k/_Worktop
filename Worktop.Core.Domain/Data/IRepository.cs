using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Worktop.Core.Domain.Data
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> FindById(int id);
        Task<T> Find(Expression<Func<T, bool>> predicate, params string[] includes);

        Task<IEnumerable<T>> GetAll(params string[] includes);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, params string[] includes);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}