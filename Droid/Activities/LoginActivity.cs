using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using SampleLogin.Droid.Helpers;
using SampleLogin.Droid.Interfaces;
using SampleLogin.Helpers;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : BaseActivity, View.IOnClickListener, IOnTextChanged
    {
        protected override int LayoutResource => Resource.Layout.activity_login;

        EditText userId;
        EditText password;
        Button signIn_btn;
        TextView newUser_btn;

        readonly LoginUserViewModel viewModel = new LoginUserViewModel();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = FindViewById<EditText>(Resource.Id.userId);
            password = FindViewById<EditText>(Resource.Id.password);
            signIn_btn = FindViewById<Button>(Resource.Id.signIn);
            newUser_btn = FindViewById<TextView>(Resource.Id.newUser);

            signIn_btn.SetOnClickListener(this);
            newUser_btn.SetOnClickListener(this);

            userId.AddTextChangedListener(new TextWatcher(this, InputField.userId));
            password.AddTextChangedListener(new TextWatcher(this, InputField.password));

            viewModel.OnLoginSuccess = LoginSucess;
            viewModel.ActivateButton = ActivateButton;
            viewModel.ShowErrorToast = ShowToast;
        }

        private void NewUser()
        {
            StartActivity(typeof(AccountInfoActivity));
        }

        private void ActivateButton(bool isEnabled)
        {
            signIn_btn.Enabled = isEnabled;
        }

        private void LoginSucess()
        {
            var validationAct = new Intent(this, typeof(ValidationActivity));
            validationAct.PutExtra(StringConstants.isValidUser, true);
            StartActivity(validationAct);
        }

        private void ShowToast(string text)
        {
            var toast = Toast.MakeText(this, text, ToastLength.Long);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }


        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case (Resource.Id.signIn):
                    {
                        HideKeyboard();
                        viewModel.TryLogin();
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

        public void SetText(string input, InputField inputField)
        {
            switch (inputField)
            {
                case InputField.userId:
                    viewModel.userId = input;
                    break;
                case InputField.password:
                    viewModel.userPassword = input;
                    break;
            }
            viewModel.TextValueChanged();
        }

        public override void OnBackPressed()
        {
            // Intentionally left it blank
        }

    }

}
