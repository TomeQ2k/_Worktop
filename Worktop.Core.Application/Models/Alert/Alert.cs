using Worktop.Core.Common.Enums;

namespace Worktop.Core.Application.Models.Alert
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