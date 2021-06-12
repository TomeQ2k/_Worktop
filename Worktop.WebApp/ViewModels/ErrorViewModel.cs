namespace Worktop.WebApp.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel()
        {
            Title = "Error";
        }
    }
}
