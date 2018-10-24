using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary.Android
{
    public class TripListFragment : ListFragment
    {
        public TripsViewModel ViewModel { get; set; }

        public TripListFragment()
        {
            ViewModel = ServiceLocator.Instance.Get<TripsViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }
        public override async void OnResume()
        {
            base.OnResume();
            await ViewModel.PullTrips();
            ListAdapter = new TripAdapter(Activity, ViewModel.Trips);
        }
        private void ToastMessage(string mssg)
            => Toast.MakeText(Activity, mssg, ToastLength.Long).Show();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //await ViewModel.PullTrips();
            //ListAdapter = new TripAdapter(Activity, ViewModel.Trips);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
#if DEBUG
            App.LogOutLn($"Selected Trip '{ViewModel.Trips[position].Name}'", GetType().Name);
#endif
            AppStore.Instance.CurrentTrip = ViewModel.Trips[position];
            Activity.StartActivity(typeof(PlaceActivity));
        }
    }
}
