using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data.Repositories.Params;

namespace Worktop.Core.Application.Params
{
    public class GetMailsParams : FilterParams, IMailFilterParams
    {
        public string Subject { get; set; }
        public bool OnlyFavorites { get; set; }
        public MailsSortType SortType { get; set; }

        public static GetMailsParams Build(string subject, bool onlyFavorites = false, MailsSortType sortType = MailsSortType.DateDescending)
            => new GetMailsParams
            {
                Subject = subject,
                OnlyFavorites = onlyFavorites,
                SortType = sortType
            };
    }
}