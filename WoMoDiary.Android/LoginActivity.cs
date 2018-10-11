
using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;
using WoMoDiary.Android;
using System.Reflection;
using I18NPortable;
using WoMoDiary.Services;
using System;

namespace WoMoDiary
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        public LoginViewModel ViewModel { get; set; }

        public LoginActivity()
        {
            App.Initialize();
            ViewModel = ServiceLocator.Instance.Get<LoginViewModel>();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            I18N.Current
                 .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                 .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
                 .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                 .SetLogger(text => System.Diagnostics.Debug.WriteLine(text)) // action to output traces
                 .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                 .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live

            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var str = prefs.GetString("UserGuid", "569DD649-F9F8-4990-B31B-45D43DDA82C2");
            var editor = prefs.Edit();
            editor.PutString("UserGuid", str);
            // editor.Commit();    // applies changes synchronously on older APIs
            editor.Apply();        // applies changes asynchronously on newer APIs
            //App.User = new User
            //{
            //    UserId = Guid.Parse(str)
            //};

            // Create your application here
            var localStore = AppStore.GetInstance();
            localStore.UserId = Guid.Parse(str);

            SetContentView(Resource.Layout.loginLayout);

            var username = FindViewById<EditText>(Resource.Id.editTextLoginUsername);
            username.TextChanged += (sender, args) => { ViewModel.Username = username.Text; };

            var password = FindViewById<EditText>(Resource.Id.editTextLoginPassword);
            password.TextChanged += (sender, args) => { ViewModel.Password = password.Text; };

            var button = FindViewById<Button>(Resource.Id.buttonLoginUser);
            button.Click += (sender, args) =>
            {
                ViewModel.LoginCommand.Execute(null);
                if (ViewModel.IsValid)
                {
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    StartActivity(typeof(NewUserActivity));
                }
            };
        }
    }
}