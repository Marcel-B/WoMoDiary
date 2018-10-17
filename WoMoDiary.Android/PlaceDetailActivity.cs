using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using WoMoDiary.Services;
using WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "PlaceDetailActivity")]
    public class PlaceDetailActivity : Activity, IOnMapReadyCallback
    {
        public GoogleMap Map { get; set; }
        public MapFragment MapFragment { get; set; }
        public Place Place { get; set; }
        public ImageView ImageViewDetailCategory { get; set; }
        public ImageView ImageViewDetailRating { get; set; }
        public TextView TextViewPlaceName { get; set; }
        public TextView TextViewPlaceDescription { get; set; }

        public void OnMapReady(GoogleMap googleMap)
        {
            Map = googleMap;
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Place.Latitude, Place.Longitude));
            marker.SetTitle(Place.Name);
            Map.AddMarker(marker);
            Map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(Place.Latitude, Place.Longitude), 15));
        }

        private void GetViews()
        {
            ImageViewDetailRating = FindViewById<ImageView>(Resource.Id.imageViewDetailRating);
            ImageViewDetailCategory = FindViewById<ImageView>(Resource.Id.imageViewDetailCategory);
            TextViewPlaceName = FindViewById<TextView>(Resource.Id.textViewDetailPlaceName);
            TextViewPlaceDescription = FindViewById<TextView>(Resource.Id.textViewDetailPlaceDescription);
            MapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentPlaceDetail);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.placeDetailLayout);
            GetViews();

            // Create your application here
            var localStore = AppStore.Instance;
            Place = localStore.CurrentPlace;
            TextViewPlaceName.Text = Place.Name;
            TextViewPlaceDescription.Text = Place.Description;
            ImageViewDetailRating.SetImageResource(Place.ToRating());
            ImageViewDetailCategory.SetImageResource(Place.ToCategory());
            MapFragment.GetMapAsync(this);
        }
    }
}