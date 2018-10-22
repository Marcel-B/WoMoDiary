using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Helpers;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "PlaceActivity")]
    public class PlaceActivity : FragmentActivity, IOnMapReadyCallback
    {
        public PlaceActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<PlacesViewModel>();
        }

        public Toolbar Toolbar { get; set; }
        public MapFragment MapFragmentPlaces { get; set; }
        public GoogleMap Map { get; set; }
        public PlacesViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activityPlacesLayout);
            GetViews();
            InitViews();
            SetViewsEvents();
        }

        private void GetViews()
        {
            MapFragmentPlaces = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentPlaces);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbarPlaces);
        }

        private void InitViews()
        {
            Toolbar.InflateMenu(Resource.Menu.addPlace);
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentPlacesFrame, new PlaceListFragment());
            transaction.Commit();
            MapFragmentPlaces.GetMapAsync(this);
        }

        private void SetViewsEvents()
        {
            Toolbar.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
            {
                var itemId = e.Item.ItemId;
                var action = Resource.Id.actionAddPlace;
                if (action == itemId)
                {
                    System.Diagnostics.Debug.WriteLine("Add Item");
                    StartActivity(typeof(NewPlaceActivity));
                }
            };
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            Map = googleMap;
            foreach (var place in ViewModel.Places)
            {
                var marker = new MarkerOptions();
                marker.SetPosition(new LatLng(place.Latitude, place.Longitude));
                marker.SetTitle(place.Name);
                Map.AddMarker(marker);
                Map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(place.Latitude, place.Longitude), 15));
            }
        }
    }
}