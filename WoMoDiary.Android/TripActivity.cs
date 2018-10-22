using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.Design.Widget;
using com.b_velop.WoMoDiary.Meta;
using Android.Widget;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class TripActivity : FragmentActivity // AppCompatActivity
    {
        public Toolbar ToolbarTrip { get; set; }
        public TabLayout TabLayoutMain { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            GetViews();
            InitViews();
            SetControllEvents();
        }

        private void GetViews()
        {
            ToolbarTrip = FindViewById<Toolbar>(Resource.Id.toolbar);
            TabLayoutMain = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
            var tab = TabLayoutMain.GetTabAt(0);
            tab.SetText(Strings.TRIPS);
            tab = TabLayoutMain.GetTabAt(1);
            tab.SetText(Strings.MAP);
        }
        private void InitViews()
        {
            ToolbarTrip.InflateMenu(Resource.Menu.addTrip);
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, new TripListFragment());
            transaction.Commit();
        }
        private void SetControllEvents()
        {
            TabLayoutMain.TabSelected += (sender, e) =>
            {
                switch (e.Tab.Position)
                {
                    case 0:
                        FragmentNavigate(new TripListFragment());
                        break;
                    case 1:
                        FragmentNavigate(new MyPlacesMapFragment());
                        break;
                }
            };
            ToolbarTrip.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
            {
                var clickedAction = e.Item.ItemId;
                var addAction = Resource.Id.action_add;
                if (clickedAction == addAction)
                    StartActivity(typeof(NewTripActivity));
            };
        }
        private void FragmentNavigate(Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }
    }
}