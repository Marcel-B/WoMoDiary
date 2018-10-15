using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;

namespace WoMoDiary
{
    public class TripListFragment : ListFragment
    {
        public TripsViewModel ViewModel { get; set; }

        public TripListFragment()
        {
            ViewModel = ServiceLocator.Instance.Get<TripsViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(Activity, mssg, ToastLength.Long).Show();

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            await ViewModel.PullTrips();
            ListAdapter = new TripAdapter(Activity, ViewModel.Trips);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Selected Trip '{ViewModel.Trips[position].Name}'");
#endif
            AppStore.Instance.CurrentTrip = ViewModel.Trips[position];
            Activity.StartActivity(typeof(PlaceActivity));
        }
    }
}
