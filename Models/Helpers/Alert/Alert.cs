using Worktop.Core.Enums;

namespace Worktop.Models.Helpers.Alert
{
    public class Alert
    {
        public AlertType Type { get; }
        public string Message { get; }

        public Alert(AlertType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}