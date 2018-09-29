using Android.OS;
using WoMoDiary.Services;
using Android.App;
using Android.Views;
using Android.Widget;
using ListFragment = Android.Support.V4.App.ListFragment;

namespace WoMoDiary.Android
{
    public class PlaceListFragment : ListFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.GetInstance();
            var trip = store.CurrentTrip;
            ListAdapter = new PlaceAdapter(Activity, trip.Places);
            // Create your fragment here
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Activity.StartActivity(typeof(PlaceDetailActivity));
        }
    }
}
