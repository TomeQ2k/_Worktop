using SendGrid.Helpers.Mail;

namespace Worktop.Models.Helpers.Email
{
    public class EmailContent
    {
        private readonly string sender;
        private readonly string receiver;

        public EmailAddress FromAddress { get; }
        public EmailAddress ToAddress { get; }

        public EmailContent(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;

            FromAddress = new EmailAddress(sender);
            ToAddress = new EmailAddress(receiver);
        }
    }
}