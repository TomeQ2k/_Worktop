using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Components
{
    public class MessageFormViewModel
    {
        [RequiredValidator]
        [StringLengthValidator(Constants.MaxContentLength)]
        public string Text { get; set; }

        public Message Message { get; private set; }

        public static MessageFormViewModel Build(Message message) => new MessageFormViewModel { Message = message };
    }
}