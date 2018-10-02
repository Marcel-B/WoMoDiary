using System;
using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Support.V4.App;
using WoMoDiary.Domain;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace WoMoDiary.Android
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : FragmentActivity // AppCompatActivity
    {
        public MainActivity()
        {
        }
        private Toolbar _toolbar;
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