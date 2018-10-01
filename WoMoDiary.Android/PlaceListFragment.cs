using System.Collections.Generic;
using Android.OS;
using WoMoDiary.Services;
using Android.App;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using ListFragment = Android.Support.V4.App.ListFragment;

namespace WoMoDiary.Android
{
    public class PlaceListFragment : ListFragment
    {
        private IList<Place> _places;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.GetInstance();
            var trip = store.CurrentTrip;
            _places = trip.Places;
            ListAdapter = new PlaceAdapter(Activity, _places);
            // Create your fragment here
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var localStore = AppStore.GetInstance();
            localStore.CurrentPlace = _places[position];
            Activity.StartActivity(typeof(PlaceDetailActivity));
        }
    }
}
