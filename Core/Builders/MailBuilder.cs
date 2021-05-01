using Worktop.Core.Builders.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Builders
{
    public class MailBuilder : IMailBuilder
    {
        private readonly Mail mail = new Mail();

        public IMailBuilder SetSubject(string subject)
        {
            this.mail.Subject = subject;

            return this;
        }

        public IMailBuilder SetContent(string content)
        {
            this.mail.Content = content;

            return this;
        }

        public IMailBuilder SentFrom(User sender)
        {
            this.mail.SenderId = sender.Id;
            this.mail.FromAddress = sender.Email;

            return this;
        }

        public IMailBuilder SentTo(User receiver)
        {
            this.mail.ReceiverId = receiver.Id;
            this.mail.ToAddress = receiver.Email;

            return this;
        }

        public Mail Build() => this.mail;
    }
}