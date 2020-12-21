
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using SampleLogin.Models;
using SampleLogin.Droid.Helpers;
using SampleLogin.ViewModels;
using System;
using static Android.App.DatePickerDialog;
using Android.Content;
using SampleLogin.Droid.Interfaces;
using Android.Telephony;
using Android.Views.InputMethods;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "AccountInfoActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class AccountInfoActivity : BaseActivity, View.IOnClickListener, iOnTextChanged, IOnDateSetListener
    {

        protected override int LayoutResource => Resource.Layout.activity_accountInfo;
        EditText firstName;
        EditText lastName;
        EditText userName;
        EditText password;
        EditText phoneNumber;
        EditText accountDatePicker;
        Button createAccount;
        AccountInfoViewModel viewModel = new AccountInfoViewModel();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            firstName = FindViewById<EditText>(Resource.Id.firstName);
            lastName = FindViewById<EditText>(Resource.Id.lastName);
            userName = FindViewById<EditText>(Resource.Id.userName);
            password = FindViewById<EditText>(Resource.Id.newPassword);
            phoneNumber = FindViewById<EditText>(Resource.Id.phonenumber);
            accountDatePicker = FindViewById<EditText>(Resource.Id.accountDatePicker);
            accountDatePicker.Text = DateTime.Now.ToShortDateString();
            createAccount = FindViewById<Button>(Resource.Id.createAccount);
            createAccount.SetOnClickListener(this);

            firstName.AddTextChangedListener(new TextWatcher(this, InputField.firstName));
            lastName.AddTextChangedListener(new TextWatcher(this, InputField.lastName));
            userName.AddTextChangedListener(new TextWatcher(this, InputField.userName));
            password.AddTextChangedListener(new TextWatcher(this, InputField.password));
            phoneNumber.AddTextChangedListener(new TextWatcher(this, InputField.phoneNumber));
            viewModel.activateCreateButton = CreateButton;
            viewModel.onCreationSuccess = AccountSucess;


            accountDatePicker.Click += delegate
            {
                OnClickDateEditText();
            };
        }

        private void CreateButton(bool isEnabled)
        {
            createAccount.Enabled = isEnabled;
        }

        private void AccountSucess()
        {
            var validationAct = new Intent(this, typeof(ValidationActivity));
            validationAct.PutExtra("isValidUser", true);
            StartActivity(validationAct);
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case (Resource.Id.createAccount):
                    {
                        var accountInfo = new AccountInfo
                        {
                            FirstName = firstName.Text,
                            LastName = lastName.Text,
                            UserName = userName.Text,
                            Password = password.Text,
                            PhoneNumber = phoneNumber.Text,
                            StartDate = GetDate(accountDatePicker.Text)
                        };
                        viewModel.createAccountAsync(accountInfo);
                        break;
                    }

                default:
                    break;
            }
        }

        private DateTime GetDate(string text)
        {
            DateTime dateTime = DateTime.Now;
            DateTime.TryParse(text, out dateTime);
            return dateTime;
        }

        private void OnClickDateEditText()
        {
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(this, this, dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day);
            datePicker.Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            accountDatePicker.Text = new DateTime(year, month + 1, dayOfMonth).ToShortDateString();
        }

        public void setText(string userInput, InputField field)
        {
            switch (field)
            {
                case InputField.firstName:
                    viewModel.firstName = userInput;
                    break;
                case InputField.lastName:
                    viewModel.lastName = userInput;
                    break;
                case InputField.password:
                    viewModel.password = userInput;
                    break;
                case InputField.userName:
                    viewModel.userName = userInput;
                    break;
                case InputField.phoneNumber:
                    if (userInput.Length == 10)
                    {
                        string phnNumber = string.Format(format: "{0:(###)###-####}", Convert.ToInt64(userInput.ToString()));
                        phoneNumber.Text = phnNumber;
                        phoneNumber.ClearFocus();
                        HideKeyboard();
                    }
                    viewModel.phoneNumber = userInput;
                    break;
                case InputField.date:
                    DateTime dateTime = DateTime.Now;
                    DateTime.TryParse(userInput, out dateTime);
                    viewModel.startDate = dateTime;
                    break;

            }
            viewModel.TextValueChanged();
        }

        public void HideKeyboard()
        {

            var inputMethodManager = this.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && this is Activity)
            {
                var token = this.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                this.Window.DecorView.ClearFocus();
            }
        }

    }
}
