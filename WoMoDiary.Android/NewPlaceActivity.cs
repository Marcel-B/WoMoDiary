using System;
using Android.App;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.ViewModels;
using Reces = Android.Resource;
using Android.Graphics;
using Android.Gms.Maps.Model;
using WoMoDiary.Helpers;
using Toast = Android.Widget.Toast;
using ToastLength = Android.Widget.ToastLength;

namespace com.b_velop.WoMoDiary.Android
{

    [Activity(Label = "NewPlaceActivity")]
    public class NewPlaceActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        public NewPlaceViewModel ViewModel { get; set; }

        public MapFragment MapFragment { get; set; }
        public GoogleMap Map { get; set; }
        public LocationManager LocationManager { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public NewPlaceActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewPlaceViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newPlaceLayout);

            GetViews();
            SetStates();
            SetControllEvents();
            Localize();


            FindViewById<EditText>(Resource.Id.editTextNewPlaceName)
                .AfterTextChanged += (sender, args) =>
                {
                    if (!(sender is EditText box)) return;
                    ViewModel.Name = box.Text;
                };

            FindViewById<EditText>(Resource.Id.editTextNewPlaceDescription)
                .AfterTextChanged += (sender, args) =>
                {
                    if (!(sender is EditText box)) return;
                    ViewModel.Description = box.Text;
                };


            var color = ImageButtonThumbUp.Background;


            var places = new Place[]
            {
                new Stellpatz(),
                new CampingPlace(),
                new Hotel(),
                new Poi(),
                new Restaurant()
            };
            var adapter = new ArrayAdapter<Place>(this,
                Reces.Layout.SimpleSpinnerItem, places);

            SpinnerPlaceCategory.Adapter = adapter;

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
                ViewModel.SavePlaceCommand.Execute(null);
                StartActivity(typeof(PlaceActivity));
            };
            // Create your application here
            LocationManager = GetSystemService(LocationService) as LocationManager;
            const string provider = LocationManager.GpsProvider;
            try
            {
                if (LocationManager.IsProviderEnabled(provider))
                    LocationManager.RequestLocationUpdates(provider, 500, 1, this);
                var location = LocationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                Latitude = location.Latitude;
                Longitude = location.Longitude;
                ViewModel.Latitude = location.Latitude;
                ViewModel.Longitude = location.Longitude;
                ViewModel.Altitude = location.Altitude;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            MapFragment.GetMapAsync(this);
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(this, mssg, ToastLength.Long).Show();

        private void Localize()
        {
            throw new NotImplementedException();
        }

        private void SetControllEvents()
        {
            ImageButtonThumbUp.Click += (sender, args) =>
            {
                ViewModel.Rating = 4;
                if (sender is ImageButton tmp)
                {
                    ImageButtonThumbUp.SetBackgroundColor(Color.Green);
                    ImageButtonThumbDown.Background = color;
                }
            };

            ImageButtonThumbDown.Click += (sender, args) =>
            {
                ViewModel.Rating = 0;
                if (sender is ImageButton tmp)
                {
                    ImageButtonThumbUp.Background = color;
                    ImageButtonThumbDown.SetBackgroundColor(Color.Green);
                }
            };

            SpinnerPlaceCategory.ItemSelected += (sender, args) =>
            {
                if (!(sender is Spinner spn)) return;
                var place = CastJavaObject.Cast<Place>(spn.SelectedItem);
                ViewModel.Type = place.Type;
            };
        }

        private void SetStates()
        {
            throw new NotImplementedException();
        }

        public ImageButton ImageButtonThumbUp { get; set; }
        public ImageButton ImageButtonThumbDown { get; set; }
        public Button ButtonSaveNewPlace { get; set; }
        public Spinner SpinnerPlaceCategory { get; set; }


        private void GetViews()
        {
            ImageButtonThumbUp = FindViewById<ImageButton>(Resource.Id.imageButtonRatingUp);
            ImageButtonThumbDown = FindViewById<ImageButton>(Resource.Id.imageButtonRatingDown);
            MapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentNewPlace);
            ButtonSaveNewPlace = FindViewById<Button>(Resource.Id.saveNewPlaceButton);
            SpinnerPlaceCategory = FindViewById<Spinner>(Resource.Id.spinnerPlaceType);
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
                ViewModel.Latitude = location.Latitude;
                ViewModel.Longitude = location.Longitude;
                ViewModel.Altitude = location.Altitude;
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
        {
            _map = googleMap;
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Latitude, Longitude));
            marker.SetTitle("Your Location");
            _map.AddMarker(marker);
            _map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(Latitude, Longitude), 66));
        }


        public void OnLocationChanged(Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            ViewModel.Latitude = location.Latitude;
            ViewModel.Longitude = location.Longitude;
            ViewModel.Altitude = location.Altitude;
            _mapFragment.GetMapAsync(this);
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
    }
}