using Worktop.Core.Common.Enums;

namespace Worktop.Core.Common.Helpers
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

        public static RoleName[] RolesToSeed = {RoleName.User, RoleName.Admin};

        #endregion

        #region viewData

        public const string ActionName = "ActionName";
        public const string PageNumber = "PageNumber";

        #endregion
    }
}