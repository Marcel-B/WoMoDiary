using Android.OS;
using WoMoDiary.Services;
using Android.App;

namespace WoMoDiary.Android
{
    [Activity(Label = "PlaceListActivity")]
    public class PlaceListActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.GetInstance();
            var trip = store.CurrentTrip;
            ListAdapter = new PlaceAdapter(this, trip.Places);
            // Create your fragment here
        }
    }
}
