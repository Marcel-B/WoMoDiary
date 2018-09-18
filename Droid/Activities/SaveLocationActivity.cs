using Android.App;
using Android.OS;
using Android.Widget;
using Android.Gms.Maps;

namespace WoMoDiary.Droid.Activities
{
    [Activity(Label = "SaveLocationActivity")]
    public class SaveLocationActivity : Activity
    {
        public MapFragment MyMapFragment { get; set; }
        public Button SaveLocationButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LocationLayout);
            SaveLocationButton = FindViewById<Button>(Resource.Id.buttonSaveLocation);

            SaveLocationButton.Click += (sender, args) =>
            {
                StartActivity(typeof(MainActivity));
            };

            MyMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentMyLocation);
            // Create your application here
        }
    }
}
