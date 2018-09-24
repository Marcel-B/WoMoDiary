using Android.App;
using Android.OS;
using Android.Widget;
using Android.Gms.Maps;
using Android.Locations;
using Android.Runtime;
using WoMoDiary.ViewModels;
using Android.Gms.Maps.Model;

namespace WoMoDiary.Droid.Activities
{
    [Activity(Label = "SaveLocationActivity")]
    public class SaveLocationActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        private SaveLocationViewModel ViewModel;
        public MapFragment MyMapFragment { get; set; }
        public Button SaveLocationButton { get; set; }
        public LocationManager MyLocationManager { get; set; }

        public SaveLocationActivity()
        {
            ViewModel = new SaveLocationViewModel();
        }

        public void OnLocationChanged(Location location)
        {
            ViewModel.Latitude = location.Latitude;
            ViewModel.Longitude = location.Longitude;
            ViewModel.Altitude = location.Altitude;
            MyMapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(ViewModel.Latitude, ViewModel.Longitude));
            marker.SetTitle("Your Location");
            googleMap.AddMarker(marker);
            googleMap.MoveCamera(
                CameraUpdateFactory.NewLatLngZoom(new LatLng(ViewModel.Latitude, ViewModel.Longitude), 10));
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }

        protected override void OnResume()
        {
            base.OnResume();
            MyLocationManager = GetSystemService(LocationService) as LocationManager;
            var provider = LocationManager.GpsProvider;
            if (MyLocationManager.IsProviderEnabled(provider))
            {
                MyLocationManager.RequestLocationUpdates(provider, 5000, 1, this);
            }

            var location = MyLocationManager.GetLastKnownLocation(LocationManager.NetworkProvider);

            if (location != null)
            {
                ViewModel.Longitude = location.Longitude;
                ViewModel.Latitude = location.Latitude;
                ViewModel.Altitude = location.Altitude;
            }
            MyMapFragment.GetMapAsync(this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            MyLocationManager.RemoveUpdates(this);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LocationLayout);
            SaveLocationButton = FindViewById<Button>(Resource.Id.buttonSaveLocation);

            SaveLocationButton.Click += (sender, args) =>
            {
                ViewModel.SaveLocationCommand.Execute(null);
                StartActivity(typeof(MainActivity));
            };

            MyMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentMyLocation);
            // Create your application here
        }

    }
}
