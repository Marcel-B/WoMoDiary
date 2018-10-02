using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Services;

namespace WoMoDiary.Android
{
    public class TripListFragment : ListFragment
    {
        private IList<Trip> _trips;
        private readonly Action<Trip> _toList;
        public TripListFragment(Action<Trip> tolist)
        {
            _toList = tolist;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            App.Initialize(Guid.NewGuid().ToString());
            var tripStore = AppStore.GetInstance();
            _trips = tripStore.Trips;
            ListAdapter = new TripAdapter(Activity, _trips);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            System.Diagnostics.Debug.WriteLine($"Selected Trip '{_trips[position].Name}'");
            var store = AppStore.GetInstance();
            store.CurrentTrip = _trips[position];
            //var transaction = FragmentManager.BeginTransaction();
            _toList?.Invoke(_trips[position] );
            //var ine = new Intent();
            //Activity.StartActivity(typeof(PlaceListFragment));
            //transaction.Replace(Resource.Id.contentFrame, new PlaceListFragment());
            //transaction.Commit();
        }
    }
}
