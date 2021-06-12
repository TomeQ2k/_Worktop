using System;

namespace Worktop.WebApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public float SalaryBonus { get; set; }
        public string JobTitle { get; set; }
        public decimal TotalSalary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateHired { get; set; }

        public ProfileViewModel()
        {
            Title = "Profile";
        }
    }
}