using System.Reflection;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using I18NPortable;

namespace WoMoDiary.Droid.Activities
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
        //protected override int LayoutResource => Resource.Layout.MainActivity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.MainActivity);

            ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText("Hello");
            tab.SetIcon(Resource.Drawable.WoMo);
            tab.TabSelected += (sender, args) =>
            {
                // Do something when tab is selected
            };
            ActionBar.AddTab(tab);

            var detailsTab = ActionBar.NewTab();
            detailsTab.SetText("Tab2");
            tab.SetIcon(Resource.Drawable.ic_save);
            detailsTab.TabSelected += (sender, args) => { };
            ActionBar.AddTab(detailsTab);

            I18N.Current
                 .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                 .SetFallbackLocale("de") // Optional but recommended: locale to load in case the system locale is not supported
                 .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                 .SetLogger(text => System.Diagnostics.Debug.WriteLine(text)) // action to output traces
                 .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                 .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live
        }
    }
}
