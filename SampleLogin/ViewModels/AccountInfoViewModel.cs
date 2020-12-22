using System;
using System.Threading.Tasks;
using Java.Util.Regex;
using SampleLogin.Helpers;
using SampleLogin.Models;

namespace SampleLogin.ViewModels
{
    public class AccountInfoViewModel : BaseViewModel
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

        public Action OnCreationSuccess { get; set; }
        public Action<bool> ActivateCreateButton { get; set; }

        public void UserInputChanged()
        {
            var hasValidFirstName = !string.IsNullOrWhiteSpace(firstName) && IsValidName(firstName);
            var hasValidLastName = !string.IsNullOrWhiteSpace(lastName) && IsValidName(lastName);
            var hasValidUserName = !string.IsNullOrWhiteSpace(userName);
            var hasValidPassword = !string.IsNullOrWhiteSpace(password) && IsValidPassword(password);
            var hasValidPhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length.Equals(10);
            if (hasValidFirstName && hasValidLastName && hasValidUserName && hasValidPassword && hasValidPhoneNumber)
            {
                ActivateCreateButton?.Invoke(true);
                return;
            }
            ActivateCreateButton?.Invoke(false);

        }

        public static bool IsValidPassword(string password)
        {
            Pattern pattern;
            Matcher matcher;
            pattern = Pattern.Compile(RegexPatterns.validPasswordRegex);
            matcher = pattern.Matcher(password);
            return matcher.Matches();
        }

        public static bool IsValidName(string name)
        {
            Pattern p = Pattern.Compile(RegexPatterns.validNameRegex);
            Matcher m = p.Matcher(name);
            bool b = !m.Find();
            return b;
        }

        private async Task CreateUser(AccountInfo info)
        {
            var isSuccess = await MockDataStore.AddItemAsync(info);
            if (isSuccess)
            {
                OnCreationSuccess?.Invoke();
                return;
            }
            ShowErrorToast?.Invoke(StringConstants.accountFailed);
        }

        public async void CreateAccountAsync()
        {
            var accountInfo = new AccountInfo
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password,
                PhoneNumber = phoneNumber,
                StartDate = startDate
            };
            await CreateUser(accountInfo);
        }

        public void DatePickerValueChanged()
        {
            bool isPastDate = startDate < DateTime.Now;
            bool isFutureDate = startDate > DateTime.Now.AddMonths(1);
            if (isPastDate || isFutureDate)
            {
                ShowErrorToast?.Invoke(StringConstants.invalidDate);
                return;
            }
            else
            {
                UserInputChanged();
            }
        }
    }
}
