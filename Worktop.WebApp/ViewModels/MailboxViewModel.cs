using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class MailboxViewModel : BaseViewModel
    {
        public PagedList<Mail> Mails { get; set; }

        public string SenderEmail { get; set; }
        public string Subject { get; set; }
        public bool OnlyFavorites { get; set; }
        public MailsSortType SortType { get; set; }

        public MailboxViewModel()
        {
            Title = "Mailbox";
        }

        public MailboxViewModel(PagedList<Mail> mails)
        {
            Title = "Mailbox";

            Mails = mails;
        }

        public MailboxViewModel FilterMails(PagedList<Mail> mails)
        {
            Mails = mails;

            return this;
        }
    }
}