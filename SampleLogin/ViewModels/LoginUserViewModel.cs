using System;
using System.Threading.Tasks;
using SampleLogin.Models;

namespace SampleLogin
{
    public class LoginUserViewModel : BaseViewModel
    {
        public LoginUserViewModel()
        {

        }
        public Action onLoginSuccess { get; set; }
        public Action<bool> activateButton { get; set; }
        public Action userDoesntExists { get; set; }

        public string userId, userPassword;

        public void tryLogin()
        {
            // Implement Db functionality
            var isUserExists = validateUser();
            var success = false;
            if (success)
            {

            }
        }

        private async Task validateUser()
        {
            var accountInfo = await MockDataStore.GetItemAsync(userId);
            if (accountInfo == null)
            {
                userDoesntExists?.Invoke();
            }

            if (accountInfo.Password == userPassword)
                onLoginSuccess?.Invoke();
           

        }

        public void TextValueChanged()
        {
            var hasValidUserNameInput = !String.IsNullOrWhiteSpace(userId);
            var hasValidPasswordInput = !String.IsNullOrWhiteSpace(userPassword);
            if(hasValidPasswordInput && hasValidUserNameInput)
            {
                activateButton?.Invoke(true);
            }
            activateButton?.Invoke(false);

        }
    }
}
