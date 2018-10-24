using System.Collections.Generic;

using Android.Gms.Maps;
using Android.OS;
using Android.Views;

using Android.Gms.Maps.Model;
using com.b_velop.WoMoDiary.Services;
using Android.Support.V4.App;
using Android.Widget;
using com.b_velop.WoMoDiary.Meta;

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

        public override void OnStart()
        {
            base.OnStart();
        }
        public override void OnResume()
        {
            base.OnResume();
            var myPlaces = Activity.FindViewById<TextView>(Resource.Id.textViewMyPlaces);
            if (myPlaces != null) myPlaces.Text = Strings.MY_PLACES;
            var mMapFragment = SupportMapFragment.NewInstance();
            FragmentTransaction fragmentTransaction =
                    FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Resource.Id.contentFrameMyMap, mMapFragment);
            fragmentTransaction.Commit();
            mMapFragment.GetMapAsync(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            => inflater.Inflate(Resource.Layout.myPlacesMapLayout, container, false);

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
            int padding = 240; // offset from edges of the map in pixels
            CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds(bounds, padding);
            Map.AnimateCamera(cu);
        }


    }
}
