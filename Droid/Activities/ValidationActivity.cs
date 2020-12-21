

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

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
            var isSuccess = Intent.Extras.GetBoolean("isValidUser");
            if (isSuccess)
            {
                statusMesg.Text = "Success";
                statusMesg.SetTextColor(Resources.GetColor(Resource.Color.green));
                statusImage.SetImageResource(Resource.Drawable.check);
            }
            else
            {
                statusMesg.Text = "Failed to Login";
                statusMesg.SetTextColor(Resources.GetColor(Resource.Color.red));
                statusImage.SetImageResource(Resource.Drawable.failed);
            }
            // Create your application here

        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
            StartActivity(typeof(LoginActivity));
        }
    }
}
