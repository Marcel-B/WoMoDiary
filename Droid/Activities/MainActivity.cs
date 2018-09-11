using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using WoMoDiary.Droid.Fragments;
using System.Reflection;
using I18NPortable;

namespace WoMoDiary.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {

        public TabWidget TabOne { get; set; }
        public TabHost MainTabHost { get; set; }
        public Button TestButton { get; set; }
        protected override int LayoutResource => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            I18N.Current
                 .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                 .SetFallbackLocale("de") // Optional but recommended: locale to load in case the system locale is not supported
                 .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                 .SetLogger(text => System.Diagnostics.Debug.WriteLine(text)) // action to output traces
                 .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                 .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live


            TabOne = FindViewById<TabWidget>(Resource.Id.tabWidget1);
            MainTabHost = FindViewById<TabHost>(Resource.Id.tabHost1);
            TestButton = FindViewById<Button>(Resource.Id.button1);

            //Toolbar.MenuItemClick += (sender, e) =>
            //{
            //    var intent = new Intent(this, typeof(AddItemActivity)); ;
            //    StartActivity(intent);
            //};

            //SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            //SupportActionBar.SetHomeButtonEnabled(false);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}
