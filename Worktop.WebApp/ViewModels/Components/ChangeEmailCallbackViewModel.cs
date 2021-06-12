using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;

namespace Worktop.WebApp.ViewModels.Components
{
    public class ChangeEmailCallbackViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}