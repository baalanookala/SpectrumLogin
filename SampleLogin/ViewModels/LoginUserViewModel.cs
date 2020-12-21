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
        public Action invalidPassword { get; set; }
        


        public string userId, userPassword;

        public async void tryLogin()
        {
            await validateUser();
        }

        private async Task validateUser()
        {
            var accountInfo = await MockDataStore.GetItemAsync(userId);
            if (accountInfo == null)
            {
                userDoesntExists?.Invoke();
            }
            else if (accountInfo.Password == userPassword)
            {
                onLoginSuccess?.Invoke();
            }
            else if (accountInfo.Password != userPassword)
            {
                invalidPassword?.Invoke();
            }
        }

        public void TextValueChanged()
        {
            var hasValidUserNameInput = !String.IsNullOrWhiteSpace(userId);
            var hasValidPasswordInput = !String.IsNullOrWhiteSpace(userPassword);
            if (hasValidPasswordInput && hasValidUserNameInput)
            {
                activateButton?.Invoke(true);
                return;
            }
            activateButton?.Invoke(false);

        }
    }
}
