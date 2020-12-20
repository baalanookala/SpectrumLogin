using System;
namespace SampleLogin.ViewModels
{
    public class AccountInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }

        public AccountInfoViewModel()
        {
        }
    }
}
