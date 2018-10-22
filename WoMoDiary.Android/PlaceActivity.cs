using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Gms.Maps;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "PlaceActivity")]
    public class PlaceActivity : FragmentActivity
    {

        public Toolbar Toolbar { get; set; }
        public MapFragment MapFragmentPlaces { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activityPlacesLayout);
            GetViews();
            InitViews();
            SetViewsEvents();
        }

        private void GetViews()
        {
            MapFragmentPlaces = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentPlaces);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbarPlaces);
        }

        private void InitViews()
        {
            Toolbar.InflateMenu(Resource.Menu.addPlace);
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentPlacesFrame, new PlaceListFragment());
            transaction.Commit();
        }

        private void SetViewsEvents()
        {
            Toolbar.MenuItemClick += (object sender, Toolbar.MenuItemClickEventArgs e) =>
            {
                var itemId = e.Item.ItemId;
                var action = Resource.Id.actionAddPlace;
                if (action == itemId)
                {
                    System.Diagnostics.Debug.WriteLine("Add Item");
                    StartActivity(typeof(NewPlaceActivity));
                }
            };
        }
    }
}