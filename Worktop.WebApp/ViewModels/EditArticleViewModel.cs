using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels
{
    public class EditArticleViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxTitleLength)]
        public string ArticleTitle { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxContentLength)]
        public string Content { get; set; }

        public EditArticleViewModel()
        {
            Title = "Article";
        }
    }
}