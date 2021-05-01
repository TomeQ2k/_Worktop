namespace Worktop.Models.Helpers.Email
{
    public class EmailMessage
    {
        public string Email { get; }
        public string Subject { get; }
        public string Message { get; }
        public string SenderEmail { get; }

        public EmailMessage(string email, string subject, string message, string senderEmail = null)
        {
            Email = email;
            Subject = subject;
            Message = message;
            SenderEmail = senderEmail;
        }
    }
}