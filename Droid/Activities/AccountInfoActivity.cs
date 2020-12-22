
using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using SampleLogin.Droid.Helpers;
using SampleLogin.Droid.Interfaces;
using SampleLogin.ViewModels;
using static Android.App.DatePickerDialog;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "AccountInfoActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class AccountInfoActivity : BaseActivity, View.IOnClickListener, IOnTextChanged, IOnDateSetListener
    {

        protected override int LayoutResource => Resource.Layout.activity_accountInfo;
        EditText firstName;
        EditText lastName;
        EditText userName;
        EditText password;
        EditText phoneNumber;
        EditText accountDatePicker;
        Button createAccount;
        readonly AccountInfoViewModel viewModel = new AccountInfoViewModel();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            firstName = FindViewById<EditText>(Resource.Id.firstName);
            lastName = FindViewById<EditText>(Resource.Id.lastName);
            userName = FindViewById<EditText>(Resource.Id.userName);
            password = FindViewById<EditText>(Resource.Id.newPassword);
            phoneNumber = FindViewById<EditText>(Resource.Id.phonenumber);
            accountDatePicker = FindViewById<EditText>(Resource.Id.accountDatePicker);
            createAccount = FindViewById<Button>(Resource.Id.createAccount);

            accountDatePicker.Text = DateTime.Now.ToShortDateString();
            createAccount.SetOnClickListener(this);

            firstName.AddTextChangedListener(new TextWatcher(this, InputField.firstName));
            lastName.AddTextChangedListener(new TextWatcher(this, InputField.lastName));
            userName.AddTextChangedListener(new TextWatcher(this, InputField.userName));
            password.AddTextChangedListener(new TextWatcher(this, InputField.password));
            phoneNumber.AddTextChangedListener(new TextWatcher(this, InputField.phoneNumber));

            viewModel.ActivateCreateButton = CreateButton;
            viewModel.OnCreationSuccess = AccountSucess;


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
                        viewModel.CreateAccountAsync();
                        break;
                    }

                default:
                    break;
            }
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

        public void SetText(string userInput, InputField field)
        {
            switch (field)
            {
                case InputField.firstName:
                    viewModel.firstName = userInput;
                    viewModel.UserInputChanged();
                    break;
                case InputField.lastName:
                    viewModel.lastName = userInput;
                    viewModel.UserInputChanged();
                    break;
                case InputField.password:
                    viewModel.password = userInput;
                    viewModel.UserInputChanged();
                    break;
                case InputField.userName:
                    viewModel.userName = userInput;
                    viewModel.UserInputChanged();
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
                    viewModel.UserInputChanged();
                    break;
                case InputField.date:
                    DateTime dateTime = DateTime.Now;
                    DateTime.TryParse(userInput, out dateTime);
                    viewModel.startDate = dateTime;
                    viewModel.DatePickerValueChanged();
                    break;

            }
        }

    }
}
