using Android.App;
using Android.OS;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using WoMoDiary.Domain;
using System.Linq;
using Android.Support.V7.App;
using Android.Support.V4.App;
using WoMoDiary.Android;
using Android.Views.Accessibility;
using System;
using Android.Content;
using Android.Preferences;
using WoMoDiary.Services;

namespace WoMoDiary
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : FragmentActivity // AppCompatActivity
    {
        public MainActivity()
        {
        }
        private Toolbar toolbar;
        protected override async void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var str = prefs.GetString("UserGuid", Guid.NewGuid().ToString());
            var editor = prefs.Edit();
            editor.PutString("UserGuid", str);
            // editor.Commit();    // applies changes synchronously on older APIs
            editor.Apply();        // applies changes asynchronously on newer APIs
            App.User = new User
            {
                Id = Guid.Parse(str)
            };
            await App.Initialize();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.InflateMenu(Resource.Menu.addTrip);
            toolbar.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
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