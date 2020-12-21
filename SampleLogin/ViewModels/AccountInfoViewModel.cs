using System;
using System.Threading.Tasks;
using Java.Util.Regex;
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
            var hasValidFirstName = !String.IsNullOrWhiteSpace(firstName) && isValidName(firstName);
            var hasValidLastName = !String.IsNullOrWhiteSpace(lastName) && isValidName(lastName);
            var hasValidUserName = !String.IsNullOrWhiteSpace(userName);
            var hasValidPassword = !String.IsNullOrWhiteSpace(password) && isValidPassword(password); 
            var hasValidPhoneNumber = !String.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length.Equals(10);
            if (hasValidFirstName && hasValidLastName && hasValidUserName && hasValidPassword && hasValidPhoneNumber)
            {
                activateCreateButton?.Invoke(true);
                return;
            }
            activateCreateButton?.Invoke(false);

        }

        public static bool isValidPassword(String password)
        {

            Pattern pattern;
            Matcher matcher;
            String PASSWORD_PATTERN = "^(?=.*[0-9])(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\\S+$).{8,15}$";
            pattern = Pattern.Compile(PASSWORD_PATTERN);
            matcher = pattern.Matcher(password);
            return matcher.Matches();

        }

        public static bool isValidName(String name)
        {

            Pattern p = Pattern.Compile("[^A-Za-z0-9]");
            Matcher m = p.Matcher(name);
            bool b = !m.Find();
            return b;

        }

        private async Task createUser(AccountInfo info)
        {
            var isSuccess = await MockDataStore.AddItemAsync(info);
            if (isSuccess)
            {
                onCreationSuccess?.Invoke();
            }
            onCreationFailure?.Invoke();

        }

        internal async void createAccountAsync(AccountInfo accountInfo)
        {
            await createUser(accountInfo);
        }
    }
}
