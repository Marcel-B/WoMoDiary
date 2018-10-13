using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using WoMoDiary.Services;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    [Activity(Label = "PlaceDetailActivity")]
    public class PlaceDetailActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap _map;
        private MapFragment _mapFragment;
        private Place _place;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(_place.Latitude, _place.Longitude));
            marker.SetTitle(_place.Name);
            _map.AddMarker(marker);
            _map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(_place.Latitude, _place.Longitude), 15));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.placeDetailLayout);

            // Create your application here
            var name = FindViewById<TextView>(Resource.Id.textViewDetailPlaceName);
            var description = FindViewById<TextView>(Resource.Id.textViewDetailPlaceDescription);
            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentPlaceDetail);

            var localStore = AppStore.GetInstance();
            _place = localStore.CurrentPlace;
            name.Text = _place.Name;
            description.Text = _place.Description;
            _mapFragment.GetMapAsync(this);
        }
    }
}