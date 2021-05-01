using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels
{
    public class MailViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [StringLengthValidator(Constants.MaxTitleLength)]
        public string Subject { get; set; }

        [RequiredValidator]
        [DataType(DataType.EmailAddress)]
        public string ToAddress { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxContentLength)]
        public string Content { get; set; }

        public IEnumerable<string> EmailAddresses { get; set; }

        public MailViewModel()
        {
            Title = "Mail";
        }

        public MailViewModel(IEnumerable<string> emailAddresses)
        {
            Title = "Mail";

            EmailAddresses = emailAddresses;
        }
    }
}