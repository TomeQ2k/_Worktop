using System.Collections.Generic;

namespace Worktop.Core.Domain.Data.Models
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        int CurrentPage { get; }
        int TotalPages { get; }
        int PageSize { get; }
        int TotalCount { get; }
    }
}