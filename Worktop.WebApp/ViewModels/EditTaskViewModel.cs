using System;
using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels
{
    public class EditTaskViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxTaskLength)]
        public string Description { get; set; }

        [RequiredValidator]
        [DataType(DataType.DateTime)]
        public DateTime DateDeadline { get; set; }

        public EditTaskViewModel()
        {
            Title = "Task";
        }
    }
}