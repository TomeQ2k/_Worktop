using Worktop.Core.Enums;
using Worktop.Models.Helpers.Alert;

namespace Worktop.Core.Services
{
    public sealed class Alertify
    {
        public static Alert Alert { get; private set; }

        public static void Push(string message, AlertType type = AlertType.Info)
        {
            Alert = new Alert(type, message);
        }

        public static void Clear()
        {
            Alert = null;
        }

        public static Alertify Build() => new Alertify();
    }
}