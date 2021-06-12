using Worktop.Core.Common.Enums;

namespace Worktop.Core.Domain.Data.Repositories.Params
{
    public interface IMailFilterParams
    {
        string Subject { get; set; }
        bool OnlyFavorites { get; set; }
        MailsSortType SortType { get; set; }
    }
}