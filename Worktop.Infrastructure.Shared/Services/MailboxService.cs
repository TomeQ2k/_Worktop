using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Models.Alert;
using Worktop.Core.Application.Models.Email;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class MailBoxService : IMailboxService
    {
        private readonly IDatabase database;
        private readonly IEmailSender emailSender;

        private readonly int currentUserId;

        public MailBoxService(IDatabase database, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;
            this.emailSender = emailSender;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<PagedList<Mail>> GetMails(GetMailsParams filterParams)
            => (PagedList<Mail>)await database.MailRepository.GetUserMails(currentUserId, (filterParams.PageNumber, filterParams.PageSize));

        public async Task<PagedList<Mail>> FilterMails(GetMailsParams filterParams)
            => (PagedList<Mail>)await database.MailRepository.GetFilteredMails(currentUserId, filterParams, (filterParams.PageNumber, filterParams.PageSize));

        public async Task<Mail> SendMail(Mail mail)
        {
            var sender = await database.UserRepository.Get(currentUserId);

            if (sender.Email.ToLower() == mail.ToAddress.ToLower())
            {
                Alertify.Push("You cannot send email to yourself", AlertType.Error);

                return null;
            }

            var receiver = await database.UserRepository.Find(u => u.Email.ToLower() == mail.ToAddress.ToLower());

            if (sender == null)
                return null;

            if (receiver == null)
            {
                Alertify.Push($"User with email address: {mail.ToAddress} does not exist", AlertType.Error);

                return null;
            }

            mail.FromAddress = sender.Email;
            mail.SenderId = currentUserId;
            mail.ReceiverId = receiver.Id;

            database.MailRepository.Add(mail);

            return await database.Complete() ? await SendExternalEmail(mail) : null;
        }

        public async Task<bool> ToggleFavorite(int mailId)
        {
            var mail = await database.MailRepository.Find(m => m.Id == mailId && ((m.SenderId == currentUserId && !m.SenderDeleted) || (m.ReceiverId == currentUserId && !m.ReceiverDeleted)));

            if (mail == null)
                return false;

            mail.IsFavorite = !mail.IsFavorite;

            return await database.Complete();
        }

        public async Task<bool> DeleteMail(int mailId)
        {
            var mail = await database.MailRepository.Find(m => m.Id == mailId && ((m.SenderId == currentUserId && !m.SenderDeleted) || (m.ReceiverId == currentUserId && !m.ReceiverDeleted)));

            if (mail == null)
                return false;

            if (mail.SenderId == currentUserId)
                mail.SenderDeleted = true;
            else
                mail.ReceiverDeleted = true;

            if (mail.SenderDeleted && mail.ReceiverDeleted)
                database.MailRepository.Delete(mail);
            else
                database.MailRepository.Update(mail);

            return await database.Complete();
        }

        public async Task<IEnumerable<string>> FetchEmailAddresses()
            => (await database.UserRepository.Fetch()).Select(u => u.Email);

        #region private

        private async Task<Mail> SendExternalEmail(Mail mail)
        {
            if (await emailSender.Send(new EmailMessage
            (
                email: mail.ToAddress,
                subject: mail.Subject,
                message: mail.Content,
                senderEmail: mail.FromAddress
            )))
                return mail;

            database.MailRepository.Delete(mail);

            if (await database.Complete())
            {
                Alertify.Push("Email was not sent to given address so it is also not persisted in database", AlertType.Error);
                return null;
            }

            Alertify.Push("Some error occurred during sending email", AlertType.Error);
            return null;
        }

        #endregion
    }
}