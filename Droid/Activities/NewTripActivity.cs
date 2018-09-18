
using Android.App;
using Android.OS;

namespace WoMoDiary.Droid
{
    [Activity(Label = "NewTripActivity")]
    public class NewTripActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewTripLayout);
            // Create your application here
        }
    }
}
