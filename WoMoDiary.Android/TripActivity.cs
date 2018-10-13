using System;
using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Support.V4.App;
using WoMoDiary.Domain;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace WoMoDiary
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class TripActivity : FragmentActivity // AppCompatActivity
    {
        private Toolbar _toolbar;
        protected override  void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.InflateMenu(Resource.Menu.addTrip);
            _toolbar.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
            {
                var itemId = e.Item.ItemId;
                var action = Resource.Id.action_add;
                if (action == itemId)
                {
                    System.Diagnostics.Debug.WriteLine("Add Item");
                    StartActivity(typeof(NewTripActivity));
                }
            };
            Action<Trip> nav = ToPlaceView;
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, new TripListFragment(ToPlaceView));
            transaction.Commit();
        }

        public void ToPlaceView(Trip place)
        {
            StartActivity(typeof(PlaceActivity));
        }
    }
}