using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels
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