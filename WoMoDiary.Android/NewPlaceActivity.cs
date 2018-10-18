using System;
using Android.App;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Widget;
using Reces = Android.Resource;
using Android.Graphics;
using Android.Gms.Maps.Model;
using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.ViewModels;
using Toast = Android.Widget.Toast;
using ToastLength = Android.Widget.ToastLength;

namespace com.b_velop.WoMoDiary.Android
{

    [Activity(Label = "NewPlaceActivity")]
    public class NewPlaceActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        public NewPlaceViewModel ViewModel { get; set; }

        public MapFragment MapFragment { get; set; }
        public GoogleMap GoogleMap { get; set; }
        public LocationManager LocationManager { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ImageButton ImageButtonThumbUp { get; set; }
        public ImageButton ImageButtonThumbDown { get; set; }
        public Button ButtonSaveNewPlace { get; set; }
        public Spinner SpinnerPlaceCategory { get; set; }
        public EditText EditTextPlaceDescription { get; set; }
        public EditText EditTextPlaceName { get; set; }

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
            var color = ImageButtonThumbUp.Background;
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
            EditTextPlaceName.AfterTextChanged += (sender, args) =>
            {
                if (!(sender is EditText box)) return;
                ViewModel.Name = box.Text;
            };
            EditTextPlaceDescription.AfterTextChanged += (sender, args) =>
            {
                if (!(sender is EditText box)) return;
                ViewModel.Description = box.Text;
            };
            ButtonSaveNewPlace.Click += (sender, args) =>
            {
                ViewModel.SavePlaceCommand.Execute(null);
                StartActivity(typeof(PlaceActivity));
            };
        }

        private void SetStates()
        {
            throw new NotImplementedException();
        }




        private void GetViews()
        {
            ImageButtonThumbUp = FindViewById<ImageButton>(Resource.Id.imageButtonRatingUp);
            ImageButtonThumbDown = FindViewById<ImageButton>(Resource.Id.imageButtonRatingDown);
            MapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentNewPlace);
            ButtonSaveNewPlace = FindViewById<Button>(Resource.Id.saveNewPlaceButton);
            SpinnerPlaceCategory = FindViewById<Spinner>(Resource.Id.spinnerPlaceType);
            EditTextPlaceDescription = FindViewById<EditText>(Resource.Id.editTextNewPlaceDescription);
            EditTextPlaceName = FindViewById<EditText>(Resource.Id.editTextNewPlaceName);
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
            MapFragment.GetMapAsync(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            LocationManager.RemoveUpdates(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GoogleMap = googleMap;
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Latitude, Longitude));
            marker.SetTitle("Your Location");
            GoogleMap.AddMarker(marker);
            GoogleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(Latitude, Longitude), 66));
        }


        public void OnLocationChanged(Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            ViewModel.Latitude = location.Latitude;
            ViewModel.Longitude = location.Longitude;
            ViewModel.Altitude = location.Altitude;
            MapFragment.GetMapAsync(this);
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
    }
}