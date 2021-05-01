using System;
using Worktop.Core.Enums;

namespace Worktop.Core.Helpers
{
    public static class Utils
    {
        #region guid

        public static string Id() => Guid.NewGuid().ToString("N").Substring(0, 16).ToUpper();

        public static string NewGuid(int length = 16) => Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);

        #endregion

        #region enum

        public static string EnumToString(RoleName roleName) => Enum.GetName(typeof(RoleName), roleName);

        #endregion
    }
}