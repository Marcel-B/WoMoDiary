using System;
using Android.App;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.ViewModels;
using Reces = Android.Resource;

namespace WoMoDiary.Android
{
    public class CastJavaObject
    {
        public static T Cast<T>(Java.Lang.Object obj) where T : Place
        {
            var propInfo = obj.GetType().GetProperty("Instance");
            return propInfo == null ? null : propInfo.GetValue(obj, null) as T;
        }
    }

    [Activity(Label = "NewPlaceActivity")]
    public class NewPlaceActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        private readonly NewPlaceViewModel _viewModel;
        private MapFragment _mapFragment;
        private GoogleMap _map;
        public LocationManager LocationManager { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public NewPlaceActivity()
        {
            _viewModel = new NewPlaceViewModel();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newPlaceLayout);
            var button = FindViewById<Button>(Resource.Id.saveNewPlaceButton);
            var spinner = FindViewById<Spinner>(Resource.Id.spinnerPlaceType);

            FindViewById<EditText>(Resource.Id.editTextNewPlaceName)
                .AfterTextChanged += (sender, args) =>
                {
                    if (!(sender is EditText box)) return;
                    _viewModel.Name = box.Text;
                }; 

            FindViewById<EditText>(Resource.Id.editTextNewPlaceDescription)
                .AfterTextChanged += (sender, args) =>
                {
                    if (!(sender is EditText box)) return;
                    _viewModel.Description = box.Text;
                };

            FindViewById<ImageButton>(Resource.Id.imageButtonRatingUp)
                .Click += (sender, args) => _viewModel.Rating = 4;

            FindViewById<ImageButton>(Resource.Id.imageButtonRatingDown)
                .Click += (sender, args) => _viewModel.Rating = 0;

            var places = new Place[]
            {
                new Stellpatz(),
                new CampingPlace(),
                new Hotel(),
                new NicePlace(),
                new Restaurant()
            };
            var adapter = new ArrayAdapter<Place>(this,
                Reces.Layout.SimpleSpinnerItem, places);

            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentNewPlace);
            spinner.Adapter = adapter;
            spinner.ItemSelected += (sender, args) =>
            {
                if (!(sender is Spinner spn)) return;
                var place = CastJavaObject.Cast<Place>(spn.SelectedItem);
                _viewModel.Type = place.Type;
            };
            //var mapOptions = new GoogleMapOptions()
            //    .InvokeMapType(GoogleMap.MapTypeSatellite)
            //    .InvokeZoomControlsEnabled(false)
            //    .InvokeCompassEnabled(true);
            //var fragTx = FragmentManager.BeginTransaction();
            //_mapFragment = MapFragment.NewInstance(mapOptions);
            //fragTx.Add(Resource.Id.mapFragmentNewPlace, _mapFragment, "mapFragmentNewPlace");
            //fragTx.Commit();
            //_mapFragment.GetMapAsync(this);
            button.Click += (sender, args) =>
            {
                _viewModel.SavePlaceCommand.Execute(null);
                StartActivity(typeof(PlaceActivity));
            };
            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            LocationManager = GetSystemService(LocationService) as LocationManager;
            const string provider = LocationManager.GpsProvider;
            try
            {
                if (LocationManager.IsProviderEnabled(provider))
                    LocationManager.RequestLocationUpdates(provider, 500, 1, this);
                var location = LocationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                Latitude = location.Latitude;
                Longitude = location.Longitude;
                _viewModel.Latitude = location.Latitude;
                _viewModel.Longitude = location.Longitude;
                _viewModel.Altitude = location.Altitude;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            _mapFragment.GetMapAsync(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            LocationManager.RemoveUpdates(this);
        }
        public void OnMapReady(GoogleMap googleMap)
            => _map = googleMap;

        public void OnLocationChanged(Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            _viewModel.Latitude = location.Latitude;
            _viewModel.Longitude = location.Longitude;
            _viewModel.Altitude = location.Altitude;
            _mapFragment.GetMapAsync(this);
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
    }
}