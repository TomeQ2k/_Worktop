using Worktop.Core.Enums;
using Worktop.Models.Helpers.Email;

namespace Worktop.Core.Helpers
{
    public class Constants
    {
        #region values

        public const int MinUserNameLength = 5;
        public const int MaxUserNameLength = 24;
        public const int MinPasswordLength = 5;
        public const int MaxPasswordLength = 30;

        public const int MaxContentLength = 500;
        public const int MaxTitleLength = 200;
        public const int MaxTaskLength = 250;
        public const int MaxDirectoryLength = 60;
        public const int MaxRoomNameLength = 50;

        public const int MinClients = 1;
        public const int MaxClients = 100;

        public const string StorageContainerHeight = "700px";
        public const string StorageHeaderContainerHeight = "20%";

        public const int MaxFilesCount = 5;
        public const int MaxFileSize = 5;

        #endregion

        #region policies

        public const string AdminPolicy = "AdminPolicy";

        #endregion

        #region roles

        public const string AdminRole = "Admin";

        public static RoleName[] RolesToSeed = { RoleName.User, RoleName.Admin };

        #endregion

        #region viewData

        public const string ActionName = "ActionName";
        public const string PageNumber = "PageNumber";

        #endregion

        #region emails

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

        #endregion
    }
}