namespace Worktop.ViewModels
{
    public class LoginViewModel : ErrorBaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Title = "Login";
        }
    }
}