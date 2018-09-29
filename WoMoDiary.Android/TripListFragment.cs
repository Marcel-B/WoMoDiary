
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using Android.Content;

namespace WoMoDiary.Android
{
    public class TripListFragment : ListFragment
    {
        IList<Trip> trips;
        Action<Trip> ToList;
        public TripListFragment(Action<Trip> tolist)
        {
            ToList = tolist;
        }
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            await App.Initialize();
            var tripStore = AppStore.GetInstance();
            trips = tripStore.Trips;
            ListAdapter = new TripAdapter(Activity, trips);
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
            System.Diagnostics.Debug.WriteLine($"Selected Trip '{trips[position].Name}'");
            // TODO - Navigate to Places List
            var store = AppStore.GetInstance();
            store.CurrentTrip = trips[position];
            //var transaction = FragmentManager.BeginTransaction();
            ToList?.Invoke(trips[position] );
            //var ine = new Intent();
            //Activity.StartActivity(typeof(PlaceListActivity));
            //transaction.Replace(Resource.Id.contentFrame, new PlaceListActivity());
            //transaction.Commit();
        }
    }
}
