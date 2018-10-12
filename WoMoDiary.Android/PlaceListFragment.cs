using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using ListFragment = Android.Support.V4.App.ListFragment;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;

namespace WoMoDiary.Android
{
    public class PlaceListFragment : ListFragment
    {
        public PlacesViewModel ViewModel { get; set; }

        public PlaceListFragment()
        {
            ViewModel = ServiceLocator.Instance.Get<PlacesViewModel>();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.GetInstance();
            var trip = store.CurrentTrip;
            ListAdapter = new PlaceAdapter(Activity, ViewModel.Places);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var localStore = AppStore.GetInstance();
            localStore.CurrentPlace = ViewModel.Places[position];
            Activity.StartActivity(typeof(PlaceDetailActivity));
        }
    }
}
