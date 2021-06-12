using Worktop.Core.Application.Models.Alert;
using Worktop.Core.Common.Enums;

namespace Worktop.WebApp.ViewModels
{
    public abstract class BaseViewModel
    {
        public string Title { get; set; }

        public Alert Alert { get; protected set; }

        public BaseViewModel WithAlert(string message, AlertType type = AlertType.Info)
        {
            Alert = new Alert(type, message);

            Alertify.Clear();

            return this;
        }

        public BaseViewModel WithAlert()
        {
            Alert = Alertify.Alert;

            Alertify.Clear();

            return this;
        }
    }
}