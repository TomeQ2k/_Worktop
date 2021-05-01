using System.ComponentModel.DataAnnotations;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels.Components
{
    public class OpinionCreatorViewModel
    {
        [RequiredValidator]
        [StringLengthValidator(Constants.MaxContentLength)]
        public string Text { get; set; }

        [RequiredValidator]
        public bool IsNegative { get; set; }

        [RequiredValidator]
        [Range(0, 100)]
        public int SalaryBonusPercentage { get; set; }

        [RequiredValidator]
        public int UserId { get; set; }
    }
}