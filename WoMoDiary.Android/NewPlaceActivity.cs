using System;
using Android;
using Android.App;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;
using Reces = Android.Resource;

namespace WoMoDiary.Android
{
    [Activity(Label = "NewPlaceActivity")]
    public class NewPlaceActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        private NewPlaceViewModel _viewModel;
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
            var foo = new Place[]
            {
                new CampingPlace(),
                new Hotel(),
                new NicePlace(),
                new Restaurant()
            };
            //var adapter = ArrayAdapter.FromArray(foo);
            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var adapter = new ArrayAdapter<Place>(this,
                Reces.Layout.SimpleSpinnerItem, foo);
            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentNewPlace);
            spinner.Adapter = adapter;
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
                StartActivity(typeof(PlaceActivity));
            };
            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            LocationManager = GetSystemService(LocationService) as LocationManager;
            var provider = LocationManager.GpsProvider;
            try
            {
                if (LocationManager.IsProviderEnabled(provider))
                    LocationManager.RequestLocationUpdates(provider, 500, 1, this);
                var location = LocationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }


            var button = FindViewById<Button>(Resource.Id.saveNewPlaceButton);
            var textName = FindViewById<EditText>(Resource.Id.editTextNewPlaceName);
            var textDescription = FindViewById<EditText>(Resource.Id.editTextNewPlaceDescription);

            button.Click += async (sender, args) =>
            {
                var place = new Place
                {
                    Name = textName.Text,
                    Description = textDescription.Text,
                    Created = DateTimeOffset.Now,
                    Type = PlaceType.Hotel,
                    Rating = 1,
                    Longitude = Longitude,
                    Latitude = Latitude
                };
                var localStore = AppStore.GetInstance();
                localStore.CurrentTrip.Places.Add(place);
                var store = ServiceLocator.Instance.Get<IDataStore<Place>>();
                await store.AddItemAsync(place);
                StartActivity(typeof(PlaceActivity));
            };


            _mapFragment.GetMapAsync(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            LocationManager.RemoveUpdates(this);
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
        }

        public void OnLocationChanged(Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            _mapFragment.GetMapAsync(this);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
    }
}