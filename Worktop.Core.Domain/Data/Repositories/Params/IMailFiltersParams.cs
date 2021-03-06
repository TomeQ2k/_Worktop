using Worktop.Core.Common.Enums;

namespace Worktop.Core.Domain.Data.Repositories.Params
{
    public interface IMailFiltersParams
    {
        string Subject { get; set; }
        bool OnlyFavorites { get; set; }
        MailsSortType SortType { get; set; }
    }
}