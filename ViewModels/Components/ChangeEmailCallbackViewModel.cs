using System.ComponentModel.DataAnnotations;
using Worktop.Core.Validators;

namespace Worktop.ViewModels.Components
{
    public class ChangeEmailCallbackViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}