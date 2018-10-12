using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;

namespace WoMoDiary.Android
{
    public class TripListFragment : ListFragment
    {
        public TripsViewModel ViewModel { get; set; }
        private readonly Action<Trip> _toList;

        public TripListFragment(Action<Trip> tolist)
        {
            _toList = tolist;
            ViewModel = ServiceLocator.Instance.Get<TripsViewModel>();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ListAdapter = new TripAdapter(Activity, ViewModel.Trips);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            System.Diagnostics.Debug.WriteLine($"Selected Trip '{ViewModel.Trips[position].Name}'");
            var store = AppStore.GetInstance();
            store.CurrentTrip = ViewModel.Trips[position];
            _toList?.Invoke(ViewModel.Trips[position]);
        }
    }
}
