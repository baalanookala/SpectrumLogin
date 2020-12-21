using System;
using System.Threading.Tasks;
using SampleLogin.Models;

namespace SampleLogin.ViewModels
{
    public class AccountInfoViewModel
    {
        public string firstName;
        public string lastName;
        public string userName;
        public string password;
        public string phoneNumber;
        public DateTime startDate;

        public AccountInfoViewModel()
        {
        }

        public Action onCreationSuccess { get; set; }
        public Action onCreationFailure { get; set; }
        public Action<bool> activateCreateButton { get; set; }

        public void TextValueChanged()
        {
            var hasValidFirstName = !String.IsNullOrWhiteSpace(firstName);
            var hasValidLastName = !String.IsNullOrWhiteSpace(lastName);
            var hasValidUserName = !String.IsNullOrWhiteSpace(userName);
            var hasValidPassword = !String.IsNullOrWhiteSpace(password);
            var hasValidPhoneNumber = !String.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length.Equals("10");
            if (hasValidFirstName && hasValidLastName)
            {
                activateCreateButton?.Invoke(true);
            }
            activateCreateButton?.Invoke(false);

        }

        private async Task createUser(AccountInfo info)
        {
            var isSuccess = await MockDataStore.AddItemAsync(info);
            if (isSuccess)
            {
                onCreationSuccess?.Invoke();
            }   onCreationFailure?.Invoke();

        }

        internal async void createAccountAsync(AccountInfo accountInfo)
        {
            await createUser(accountInfo);
        }
    }
}
