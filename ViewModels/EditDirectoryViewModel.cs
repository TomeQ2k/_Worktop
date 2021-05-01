using Worktop.Models.Domain;

namespace Worktop.ViewModels
{
    public class EditDirectoryViewModel : ErrorBaseViewModel
    {
        public Directory Directory { get; set; }

        public EditDirectoryViewModel(Directory directory)
        {
            Title = "Edit directory";

            Directory = directory;
        }
    }
}