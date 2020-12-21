using System;
using System.Threading.Tasks;

namespace SampleLogin
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel()
        {

        }
        public Action onLoginSuccess { get; set; }
        public Action<bool> activateButton { get; set; }
        public Action userDoesntExists { get; set; }

        public string userId, userPassword;

        public async void tryLogin()
        {
            await validateUser();
            // Implement Db functionality
            //var isUserExists = validateUser();
            
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
            if (hasValidPasswordInput && hasValidUserNameInput)
            {
                activateButton?.Invoke(true);
            }
            activateButton?.Invoke(false);

        }
    }
}
