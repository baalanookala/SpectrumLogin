using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using System;
using SampleLogin.Models;
using SampleLogin.Droid.Helpers;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : BaseActivity, View.IOnClickListener
    {
        protected override int LayoutResource => Resource.Layout.activity_login;
        EditText userId;
        EditText password;
        Button signIn_btn;
        Button newUser_btn;
        LoginUserViewModel viewModel = new LoginUserViewModel();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = FindViewById<EditText>(Resource.Id.userId);
            password = FindViewById<EditText>(Resource.Id.password);

            userId.AddTextChangedListener(new TextWatcher(viewModel.userId, viewModel.TextValueChanged));
            password.AddTextChangedListener(new TextWatcher(viewModel.userPassword, viewModel.TextValueChanged));
            viewModel.onLoginSuccess = LoginSucess;
            viewModel.activateButton = ActivateButton;

            // Create your application here
        }

        private void ActivateButton(bool isEnabled)
        {
            signIn_btn.Enabled = isEnabled;
        }

        private void LoginSucess()
        {
            // Trigger an intent to launch new Activity
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
                        viewModel.tryLogin();
                        break;
                    }
                case (Resource.Id.newUser):
                    {
                        break;
                    }
                default:
                    break;
            }
        }


    }
}
