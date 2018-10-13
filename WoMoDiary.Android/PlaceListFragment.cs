using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using ListFragment = Android.Support.V4.App.ListFragment;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using Toast = Android.Widget.Toast;
using ToastLength = Android.Widget.ToastLength;

namespace WoMoDiary
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


        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.GetInstance();
            var trip = store.CurrentTrip;
            await ViewModel.PullPlaces(trip.Id);
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
