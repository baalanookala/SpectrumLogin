﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace SampleLogin.Droid
{
    public class BaseActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(LayoutResource);


            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (Toolbar != null)
            {
                SetSupportActionBar(Toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);

            }
        }

        public Android.Support.V7.Widget.Toolbar Toolbar
        {
            get;
            set;
        }

        protected virtual int LayoutResource
        {
            get;
        }

        protected int ActionBarIcon
        {
            set { Toolbar?.SetNavigationIcon(value); }
        }

        protected void HideKeyboard()
        {
            var inputMethodManager = this.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && this is Activity)
            {
                var token = this.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                this.Window.DecorView.ClearFocus();
            }
        }

        public void ShowToast(string text)
        {
            var toast = Toast.MakeText(this, text, ToastLength.Long);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }
    }
}
