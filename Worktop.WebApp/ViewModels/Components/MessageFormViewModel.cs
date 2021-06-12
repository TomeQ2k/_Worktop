using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels.Components
{
    public class MessageFormViewModel
    {
        [RequiredValidator]
        [StringLengthValidator(Constants.MaxContentLength)]
        public string Text { get; set; }
    }
}