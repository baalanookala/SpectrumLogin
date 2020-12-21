using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using SampleLogin.Droid.Helpers;
using SampleLogin.Droid.Interfaces;
using SampleLogin.Models;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : BaseActivity, View.IOnClickListener, iOnTextChanged
    {
        protected override int LayoutResource => Resource.Layout.activity_login;
        EditText userId;
        EditText password;
        Button signIn_btn;
        TextView newUser_btn;
        LoginUserViewModel viewModel = new LoginUserViewModel();
        TextWatcher userIdTextWatcher = new TextWatcher();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = FindViewById<EditText>(Resource.Id.userId);
            password = FindViewById<EditText>(Resource.Id.password);
            signIn_btn = FindViewById<Button>(Resource.Id.signIn);
            signIn_btn.SetOnClickListener(this);
            newUser_btn = FindViewById<TextView>(Resource.Id.newUser);
            newUser_btn.SetOnClickListener(this);

            userId.AddTextChangedListener(new TextWatcher(this, InputField.userId));
            password.AddTextChangedListener(new TextWatcher(this, InputField.password));
            viewModel.onLoginSuccess = loginSucess;
            viewModel.activateButton = activateButton;
            viewModel.userDoesntExists = invalidUser;
            viewModel.invalidPassword = invalidPassword;
        }

        private void NewUser()
        {
            StartActivity(typeof(AccountInfoActivity));
        }

        private void activateButton(bool isEnabled)
        {
            if (isEnabled)
            {
                signIn_btn.Enabled = true;
            }
            else
            {
                signIn_btn.Enabled = false;
            }

        }

        private void loginSucess()
        {
            var validationAct = new Intent(this, typeof(ValidationActivity));
            validationAct.PutExtra("isValidUser", true);
            StartActivity(validationAct);
        }

        private void invalidUser()
        {
            showToast("User Doesnt Exist");
        }

        private void invalidPassword()
        {
            showToast("Password Doesn't match");
        }

        private void showToast(string text)
        {
            var toast = Toast.MakeText(this, text, ToastLength.Long);
            toast.Show();
        }


        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case (Resource.Id.signIn):
                    {
                        var loginInfo = new LoginInfo
                        {
                            Id = userId.Text,
                            Password = password.Text
                        };
                        HideKeyboard();
                        viewModel.tryLogin();
                        break;
                    }
                case (Resource.Id.newUser):
                    {
                        NewUser();
                        break;
                    }
                default:
                    break;
            }
        }

        public void setText(string x, InputField y)
        {
            switch (y)
            {
                case InputField.userId:
                    viewModel.userId = x;
                    break;
                case InputField.password:
                    viewModel.userPassword = x;
                    break;
            }
            viewModel.TextValueChanged();
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
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
