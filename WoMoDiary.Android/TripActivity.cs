using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class TripActivity : FragmentActivity // AppCompatActivity
    {
        public Toolbar ToolbarTrip { get; set; }

        private void GetViews()
        {
            ToolbarTrip = FindViewById<Toolbar>(Resource.Id.toolbar);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            GetViews();

            ToolbarTrip.InflateMenu(Resource.Menu.addTrip);
            ToolbarTrip.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
            {
                var clickedAction = e.Item.ItemId;
                var addAction = Resource.Id.action_add;
                if (clickedAction == addAction)
                    StartActivity(typeof(NewTripActivity));
            };
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, new TripListFragment());
            transaction.Commit();
        }
    }
}