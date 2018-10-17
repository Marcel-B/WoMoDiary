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
        public NewPlaceViewModel ViewModel { get; set; }

        private MapFragment _mapFragment;
        private GoogleMap _map;
        public LocationManager LocationManager { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public NewPlaceActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewPlaceViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(this, mssg, ToastLength.Long).Show();

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
                    ViewModel.Name = box.Text;
                };

            FindViewById<EditText>(Resource.Id.editTextNewPlaceDescription)
                .AfterTextChanged += (sender, args) =>
                {
                    if (!(sender is EditText box)) return;
                    ViewModel.Description = box.Text;
                };

            var imgBtnUp = FindViewById<ImageButton>(Resource.Id.imageButtonRatingUp);
            var imgBtnDown = FindViewById<ImageButton>(Resource.Id.imageButtonRatingDown);
            var color = imgBtnUp.Background;
            imgBtnUp.Click += (sender, args) =>
                {
                    ViewModel.Rating = 4;
                    if (sender is ImageButton tmp)
                    {
                        imgBtnUp.SetBackgroundColor(Color.Green);
                        imgBtnDown.Background = color;
                    }
                };

            imgBtnDown.Click += (sender, args) =>
                {
                    ViewModel.Rating = 0;
                    if (sender is ImageButton tmp)
                    {
                        imgBtnUp.Background = color;
                        imgBtnDown.SetBackgroundColor(Color.Green);
                    }
                };

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

            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentNewPlace);
            spinner.Adapter = adapter;
            spinner.ItemSelected += (sender, args) =>
            {
                if (!(sender is Spinner spn)) return;
                var place = CastJavaObject.Cast<Place>(spn.SelectedItem);
                ViewModel.Type = place.Type;
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
            _mapFragment.GetMapAsync(this);
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