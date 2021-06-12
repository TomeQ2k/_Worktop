using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data.Repositories.Params;

namespace Worktop.Core.Application.Params
{
    public class MailFiltersParams : FiltersParams, IMailFiltersParams
    {
        public string Subject { get; set; }
        public bool OnlyFavorites { get; set; }
        public MailsSortType SortType { get; set; }

        public static MailFiltersParams Build(string subject, bool onlyFavorites = false, MailsSortType sortType = MailsSortType.DateDescending)
            => new MailFiltersParams
            {
                Subject = subject,
                OnlyFavorites = onlyFavorites,
                SortType = sortType
            };
    }
}