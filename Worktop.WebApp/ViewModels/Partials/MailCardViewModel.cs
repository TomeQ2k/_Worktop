using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class MailCardViewModel
    {
        public Mail Mail { get; private set; }

        public int CurrentPage { get; private set; }

        public static MailCardViewModel Build(Mail mail, int currentPage = 1)
            => new MailCardViewModel
            {
                Mail = mail,
                CurrentPage = currentPage
            };
    }
}