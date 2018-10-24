using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using System;

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
        public PlacesViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activityPlacesLayout);
        }
        protected override void OnResume()
        {
            base.OnResume();
            GetViews();
            InitViews();
            SetViewsEvents();
        }
        private void GetViews()
        {
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbarPlaces);
        }

        private void InitViews()
        {
            Toolbar.InflateMenu(Resource.Menu.addPlace);
            var transaction = SupportFragmentManager.BeginTransaction();
            var mMapFragment = SupportMapFragment.NewInstance();

            transaction.Add(Resource.Id.contentFramePlacesMap, mMapFragment);
            transaction.Add(Resource.Id.contentPlacesFrame, new PlaceListFragment());

            transaction.Commit();

            mMapFragment.GetMapAsync(this);
        }

        private void SetViewsEvents()
        {
            Toolbar.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
            {
                var itemId = e.Item.ItemId;
                var action = Resource.Id.actionAddPlace;
                if (action == itemId)
                {
                    App.LogOutLn("Add Item", GetType().Name);
                    StartActivity(typeof(NewPlaceActivity));
                }
            };
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            try
            {
                var markers = new List<MarkerOptions>();
                foreach (var place in ViewModel.Places)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(place.Latitude, place.Longitude));
                    marker.SetTitle(place.Name);
                    googleMap.AddMarker(marker);
                    markers.Add(marker);
                }
                if (markers.Count == 0) return;
                var builder = new LatLngBounds.Builder();

                foreach (var marker in markers)
                    builder.Include(marker.Position);

                LatLngBounds bounds = builder.Build();
                int padding = 145; // offset from edges of the map in pixels
                var cu = CameraUpdateFactory.NewLatLngBounds(bounds, padding);
                googleMap.AnimateCamera(cu);
            }
            catch (Exception e)
            {
                App.LogOutLn(e.StackTrace, GetType().Name);
            }
        }
    }
}