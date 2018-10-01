using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;

namespace WoMoDiary.Android
{
    [Activity(Label = "PlaceActivity")]
    public class PlaceActivity : FragmentActivity
    {
        private Toolbar Toolbar { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activityPlacesLayout);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbarPlaces);
            Toolbar.InflateMenu(Resource.Menu.addPlace);
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
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentPlacesFrame, new PlaceListFragment());
            transaction.Commit();
            // Create your application here
        }
    }
}