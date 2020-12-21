
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using SampleLogin.Models;
using SampleLogin.Droid.Helpers;
using SampleLogin.ViewModels;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "AccountInfoActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class AccountInfoActivity : BaseActivity, View.IOnClickListener
    {

        protected override int LayoutResource => Resource.Layout.activity_accountInfo;
        EditText firstName;
        EditText lastName;
        EditText userName;
        EditText password;
        EditText phoneNumber;
        DatePicker datePicker;
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
            datePicker = FindViewById<DatePicker>(Resource.Id.accountDatePicker);
            createAccount = FindViewById<Button>(Resource.Id.createAccount);

            firstName.AddTextChangedListener(new TextWatcher(viewModel.firstName, viewModel.TextValueChanged));
            lastName.AddTextChangedListener(new TextWatcher(viewModel.lastName, viewModel.TextValueChanged));
            userName.AddTextChangedListener(new TextWatcher(viewModel.userName, viewModel.TextValueChanged));
            password.AddTextChangedListener(new TextWatcher(viewModel.password, viewModel.TextValueChanged));
            phoneNumber.AddTextChangedListener(new TextWatcher(viewModel.phoneNumber, viewModel.TextValueChanged));
            viewModel.activateCreateButton = CreateButton;
            viewModel.onCreationSuccess = AccountSucess;

        }

        private void CreateButton(bool isEnabled)
        {
            createAccount.Enabled = isEnabled;
        }

        private void AccountSucess()
        {
            // Trigger an intent to launch new Activity
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
                            StartDate = datePicker.DateTime
                        };
                        viewModel.createAccountAsync(accountInfo);
                        break;
                    }
               
                default:
                    break;
            }
        }
    }
}
