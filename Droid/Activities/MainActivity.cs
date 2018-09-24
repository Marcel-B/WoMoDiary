using Android.App;
using Android.OS;
using System.Reflection;
using I18NPortable;
using WoMoDiary.Droid.Fragments;
using WoMoDiary.Droid.Activities;
using WoMoDiary.Droid.Adapter;
using System.Linq;

namespace WoMoDiary.Droid
{
    [Activity(Label = "WoMo", MainLauncher = true, Icon = "@mipmap/icon")]
    //public class MainActivity : Android.Support.V4.App.FragmentActivity
    public class MainActivity : ListActivity
    {
        //public Android.Support.Design.Widget.TabLayout TabLayout { get; set; }
        //public Android.Support.V7.Widget.Toolbar Toolbar { get; set; }

        async protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.MainLayout);
            //var store = MockDataStore.GetInstance();
            var store  = new  CloudDataStore();
            ListAdapter = new TripAdapter(this, (await store.GetItemsAsync(true)).ToList());
            I18N.Current
                 .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                 .SetFallbackLocale("de") // Optional but recommended: locale to load in case the system locale is not supported
                 .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                 .SetLogger(text => System.Diagnostics.Debug.WriteLine(text)) // action to output traces
                 .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                 .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live

            //TabLayout = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.mainTabLayout);
            //Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarMain);
            //Toolbar.InflateMenu(Resource.Menu.top_menus);
            //Toolbar.MenuItemClick += (sender, e) =>
            //{
            //    var id = e.Item.ItemId;

            //    switch (id)
            //    {
            //        case Resource.Id.menuAddTrip:
            //            StartActivity(typeof(NewTripActivity));
            //            break;
            //        case Resource.Id.menuSaveLocation:
            //            StartActivity(typeof(SaveLocationActivity));
            //            break;
            //    }
            //};

            //TabLayout.TabSelected += (sender, e) =>
            //{
            //    switch (e.Tab.Position)
            //    {
            //        case 0:
            //            FragmentNavigate(new TripsFragment());
            //            break;
            //        case 1:
            //            FragmentNavigate(new LocationFragment());
            //            break;
            //        case 2:
            //            break;
            //        case 3:
            //            break;
            //    }
            //};
            //FragmentNavigate(new TripsFragment());
        }

        private void FragmentNavigate(Android.Support.V4.App.Fragment fragment)
        {
            //var transaction = SupportFragmentManager.BeginTransaction();
            //transaction.Replace(Resource.Id.contentFrame, fragment);
            //transaction.Commit();
        }
    }
}
