using Android.App;
using Android.OS;
using Android.Widget;
using SampleLogin.Helpers;

namespace SampleLogin.Droid.Activities
{
    [Activity(Label = "ValidationActivity")]
    public class ValidationActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_validation;

        TextView statusMesg;
        ImageView statusImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            statusImage = FindViewById<ImageView>(Resource.Id.statusImage);
            statusMesg = FindViewById<TextView>(Resource.Id.status);

            var isSuccess = Intent.Extras.GetBoolean(StringConstants.isValidUser);

            if (isSuccess)
            {
                statusMesg.Text = StringConstants.success;
                statusImage.SetImageResource(Resource.Drawable.check);
            }
            else
            {
                statusMesg.Text = StringConstants.failedLogin;
                statusImage.SetImageResource(Resource.Drawable.failed);
            }
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(LoginActivity));
        }
    }
}
