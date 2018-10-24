using System;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Widget;
using Reces = Android.Resource;
using Toast = Android.Widget.Toast;
using ToastLength = Android.Widget.ToastLength;

using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary.Android
{

    [Activity(Label = "NewPlaceActivity")]
    public class NewPlaceActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        public NewPlaceActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewPlaceViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }

        public NewPlaceViewModel ViewModel { get; set; }

        public MapFragment MapFragment { get; set; }
        public GoogleMap GoogleMap { get; set; }
        public LocationManager LocationManager { get; set; }

        public ImageButton ImageButtonThumbUp { get; set; }
        public ImageButton ImageButtonThumbDown { get; set; }

        public Button ButtonSaveNewPlace { get; set; }
        public Spinner SpinnerPlaceCategory { get; set; }
        public EditText EditTextPlaceDescription { get; set; }
        public EditText EditTextPlaceName { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newPlaceLayout);

            GetViews();
            SetStates();
            SetControllEvents();
            Localize();
            MapFragment.GetMapAsync(this);
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(this, mssg, ToastLength.Long).Show();

        private void Localize()
        {
            ButtonSaveNewPlace.Text = Strings.SAVE_POSITION;
            EditTextPlaceName.Hint = Strings.ENTER_NAME;
            EditTextPlaceDescription.Hint = Strings.ENTER_DESCRIPTION;
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
            };
        }
        private void SavePlaceReady(bool status)
        {
            if (!status) return;
            Finish();
        }
        private void SetStates()
        {
            ViewModel.SavePlaceSuccessCallback = SavePlaceReady;
            var places = new Place[]
             {
                new MotorhomePlace(),
                new CampingPlace(),
                new Hotel(),
                new Poi(),
                new Restaurant()
             };
            var adapter = new ArrayAdapter<Place>(this,
                Reces.Layout.SimpleSpinnerItem, places);

            SpinnerPlaceCategory.Adapter = adapter;

            LocationManager = GetSystemService(LocationService) as LocationManager;
            const string provider = LocationManager.GpsProvider;
            try
            {
                if (LocationManager.IsProviderEnabled(provider))
                    LocationManager.RequestLocationUpdates(provider, 500, 1, this);
                var location = LocationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                if (location == null)
                {
                    App.LogOutLn("No location", GetType().Name);
                }
                else
                {
                    ViewModel.Latitude = location.Latitude;
                    ViewModel.Longitude = location.Longitude;
                    ViewModel.Altitude = location.Altitude;
                }

            }
            catch (Exception e)
            {
                App.LogOutLn(e.Message, GetType().Name);
            }
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
                if (location == null)
                {
                    App.LogOutLn("No Location received", GetType().Name);
                }
                else
                {
                    ViewModel.Latitude = location.Latitude;
                    ViewModel.Longitude = location.Longitude;
                    ViewModel.Altitude = location.Altitude;
                }
            }
            catch (Exception e)
            {
                App.LogOutLn(e.Message, GetType().Name);
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
            GoogleMap.MapType = GoogleMap.MapTypeNormal;
            GoogleMap.PoiClick += (sender, e) =>
            {
                EditTextPlaceName.Text = e.Poi.Name;
                ViewModel.Longitude = e.Poi.LatLng.Longitude;
                ViewModel.Latitude = e.Poi.LatLng.Latitude;
                MapFragment.GetMapAsync(this);
            };
            // to remove old markers
            GoogleMap.Clear();

            //var mapOptions = new GoogleMapOptions()
            //.InvokeMapType(GoogleMap.MapTypeSatellite)
            //.InvokeZoomControlsEnabled(false)
            //.InvokeCompassEnabled(true);

            //            GoogleMap.SetMapStyle(new MapStyleOptions(@"[{""featureType"": ""all"",
            //    ""stylers"": [
            //      { ""color"": ""#C0C0C0"" }
            //    ]
            //  },{
            //    ""featureType"": ""road.arterial"",
            //    ""elementType"": ""geometry"",
            //    ""stylers"": [
            //      { ""color"": ""#CCFFFF"" }
            //    ]
            //  },{
            //    ""featureType"": ""landscape"",
            //    ""elementType"": ""labels"",
            //    ""stylers"": [
            //      { ""visibility"": ""off"" }
            //    ]
            //  }
            //]"));
            GoogleMap.UiSettings.MyLocationButtonEnabled = true;

            var marker = GoogleMap.AddMarker(
                new MarkerOptions()
                    .SetPosition(new LatLng(ViewModel.Latitude, ViewModel.Longitude))
                    .SetTitle(Strings.YOU_ARE_HERE)
            );

            GoogleMap.MoveCamera(
                CameraUpdateFactory.NewLatLngZoom(
                    new LatLng(ViewModel.Latitude, ViewModel.Longitude), 17));
        }

        public void OnLocationChanged(Location location)
        {
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