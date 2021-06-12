using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;

namespace Worktop.Core.Application.Extensions
{
    public static class CollectionExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> collection, int pageNumber, int pageSize) where T : class, new()
            => PagedList<T>.Create(collection.AsQueryable(), pageNumber, pageSize);

        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> collection, int pageNumber, int pageSize) where T : class
            => await PagedList<T>.CreateAsync(collection, pageNumber, pageSize);
    }
}