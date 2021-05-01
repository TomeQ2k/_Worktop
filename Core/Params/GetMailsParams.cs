using Worktop.Core.Enums;

namespace Worktop.Core.Params
{
    public class GetMailsParams : FilterParams
    {
        public string Subject { get; private set; }
        public bool OnlyFavorites { get; private set; }
        public MailsSortType SortType { get; private set; }

        public static GetMailsParams Build(string subject, bool onlyFavorites = false, MailsSortType sortType = MailsSortType.DateDescending)
            => new GetMailsParams
            {
                Subject = subject,
                OnlyFavorites = onlyFavorites,
                SortType = sortType
            };
    }
}