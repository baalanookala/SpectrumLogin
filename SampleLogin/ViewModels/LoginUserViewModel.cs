using System;
using System.Threading.Tasks;
using SampleLogin.Helpers;

namespace SampleLogin
{
    public class LoginUserViewModel : BaseViewModel
    {

        public LoginUserViewModel()
        {

        }
        public Action OnLoginSuccess { get; set; }
        public Action<bool> ActivateButton { get; set; }

        public string userId;
        public string userPassword;

        public async void TryLogin()
        {
            await ValidateUser();
        }

        private async Task ValidateUser()
        {
            var accountInfo = await MockDataStore.GetItemAsync(userId);
            if (accountInfo == null)
            {
                ShowErrorToast?.Invoke(StringConstants.noUser);
            }
            else if (accountInfo.Password == userPassword)
            {
                OnLoginSuccess?.Invoke();
            }
            else if (accountInfo.Password != userPassword)
            {
                ShowErrorToast?.Invoke(StringConstants.wrongPassword);
            }
        }

        public void TextValueChanged()
        {
            var hasValidUserNameInput = !string.IsNullOrWhiteSpace(userId);
            var hasValidPasswordInput = !string.IsNullOrWhiteSpace(userPassword);
            if (hasValidPasswordInput && hasValidUserNameInput)
            {
                ActivateButton?.Invoke(true);
                return;
            }
            ActivateButton?.Invoke(false);

        }
    }
}
