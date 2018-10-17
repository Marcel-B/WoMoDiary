using Android.OS;
using Android.Views;
using Android.Widget;
using WoMoDiary.Services;
using ListFragment = Android.Support.V4.App.ListFragment;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using Toast = Android.Widget.Toast;
using ToastLength = Android.Widget.ToastLength;

namespace com.b_velop.WoMoDiary.Android
{
    public class PlaceListFragment : ListFragment
    {
        public PlacesViewModel ViewModel { get; set; }

        public PlaceListFragment()
        {
            ViewModel = ServiceLocator.Instance.Get<PlacesViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(Activity, mssg, ToastLength.Long).Show();


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.Instance;
            var trip = store.CurrentTrip;
            ViewModel.PullPlaces();
            ListAdapter = new PlaceAdapter(Activity, ViewModel.Places);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var localStore = AppStore.Instance;
            localStore.CurrentPlace = ViewModel.Places[position];
            Activity.StartActivity(typeof(PlaceDetailActivity));
        }
    }
}
