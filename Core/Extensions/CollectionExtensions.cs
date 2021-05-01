using System.Collections.Generic;
using System.Linq;
using Worktop.Models.Helpers.Pagination;

namespace Worktop.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> collection, int pageNumber, int pageSize) where T : class, new()
                    => PagedList<T>.Create(collection.AsQueryable(), pageNumber, pageSize);
    }
}