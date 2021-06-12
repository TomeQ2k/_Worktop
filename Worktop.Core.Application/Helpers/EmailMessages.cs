using Worktop.Core.Application.Models.Email;

namespace Worktop.Core.Application.Helpers
{
    public static class EmailMessages
    {
        public static EmailMessage ActivationAccountEmail(string email, string callbackUrl)
            => new EmailMessage(
                email: email,
                subject: "Worktop - activate your account",
                message: $"<p>Hi <strong>{email}</strong>!</p><br><br>" +
                         $"<p>In order to activate your account on Worktop, click link below.<br><br>" +
                         $"Activation account link: <a href='{callbackUrl}'>LINK</a></p>" +
                         "<p>Best regards,<br>" +
                         "Worktop team</p>"
            );

        public static EmailMessage ResetPasswordEmail(string email, string callbackUrl)
            => new EmailMessage(
                email: email,
                subject: "Worktop - reset password",
                message: $"<p>Hi <strong>{email}</strong>!</p><br><br>" +
                         $"<p>In order to reset your password on Worktop, click link below.<br><br>" +
                         $"Reset password link: <a href='{callbackUrl}'>LINK</a></p>" +
                         "<p>Best regards,<br>" +
                         "Worktop team</p>"
            );

        public static EmailMessage EmailChangeEmail(string email, string callbackUrl)
            => new EmailMessage(
                email: email,
                subject: "Worktop - change email",
                message: $"<p>Hi <strong>{email}</strong>!</p><br><br>" +
                         $"<p>In order to change your email on Worktop, click link below.<br><br>" +
                         $"Change email link: <a href='{callbackUrl}'>LINK</a></p>" +
                         "<p>Best regards,<br>" +
                         "Worktop team</p>"
            );
    }
}