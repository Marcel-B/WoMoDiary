using System.Collections.Generic;

using Android.Gms.Maps;
using Android.OS;
using Android.Views;

using Android.Gms.Maps.Model;
using com.b_velop.WoMoDiary.Services;
using Android.Support.V4.App;

namespace com.b_velop.WoMoDiary.Android
{
    public class MyPlacesMapFragment : Fragment, IOnMapReadyCallback
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // contentFrameMyMap



            //FragmentTransaction fragmentTransaction =
            //        getFragmentManager().beginTransaction();
            //fragmentTransaction.add(R.id.my_container, mMapFragment);
            //fragmentTransaction.commit();


            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

             var view = inflater.Inflate(Resource.Layout.myPlacesMapLayout, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);
            //View view = inflater.Inflate(Resource.Layout.myPlacesMapLayout, container, false);
            var mMapFragment = SupportMapFragment.NewInstance();
            FragmentTransaction fragmentTransaction =
                    FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.contentFrameMyMap, mMapFragment);
            ////fragmentTransaction.commit();

            //// init
            //var mapFragment = (SupportMapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFragmentMyPlaces);
            //// don't recreate fragment everytime ensure last map location/state are maintain
            //if (mapFragment == null)
            //{
            //    mapFragment = SupportMapFragment.NewInstance();
            //}
            //var transaction = FragmentManager.BeginTransaction();
            //// R.id.map is a layout
            //transaction.Replace(Resource.Id.mapFragmentMyPlaces, mapFragment).Commit();
            mMapFragment.GetMapAsync(this);
            return view;
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            var Map = googleMap;
            IList<MarkerOptions> markers = new List<MarkerOptions>();
            var trips = AppStore.Instance.User.Trips;
            Map.Clear();
            foreach (var trip in trips)
            {
                var places = trip.Places;
                foreach (var place in places)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(place.Latitude, place.Longitude));
                    marker.SetTitle(place.Name);
                    Map.AddMarker(marker);
                    markers.Add(marker);
                    //Map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(place.Latitude, place.Longitude), 15));
                }

            }
            LatLngBounds.Builder builder = new LatLngBounds.Builder();
            foreach (var marker in markers)
            {
                builder.Include(marker.Position);
            }
            LatLngBounds bounds = builder.Build();
            int padding = 40; // offset from edges of the map in pixels
            CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds(bounds, padding);
            Map.AnimateCamera(cu);
        }
    }
}
