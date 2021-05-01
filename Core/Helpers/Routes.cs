namespace Worktop.Core.Helpers
{
    public static class Routes
    {
        public const string ErrorRoute = "/Home/Error";

        public const string LoginRoute = "/Auth/Login";
        public const string LogoutRoute = "/Auth/Logout";
        public const string AccessDeniedRoute = "/Auth/AccessDenied";

        public const string MailboxHubRoute = "/MailboxHub";
        public const string GlobalChatHubRoute = "/ChatHub/Global";
        public const string RoomsChatHubRoute = "/ChatHub/Rooms";
    }
}