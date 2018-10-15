using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;
using System.Reflection;
using I18NPortable;
using WoMoDiary.Services;

namespace WoMoDiary
{
    [Activity(Label = "WoMo Diary", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        public LoginViewModel ViewModel { get; set; }
        public Button ButtonLogin { get; set; }
        public Button ButtonNewUser { get; set; }
        public EditText EditTextLoginPassword { get; set; }
        public EditText EditTextLoginUsername { get; set; }

        public LoginActivity()
        {
            App.Initialize();
            ViewModel = ServiceLocator.Instance.Get<LoginViewModel>();
            ViewModel.LoginReady = IsReady;
            ViewModel.ErrorAction = ToastMessage;
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(this, mssg, ToastLength.Long).Show();

        private void IsReady(bool isValid)
        {
            if (isValid)
            {
                StartActivity(typeof(TripActivity));
            }
            else
            {
                StartActivity(typeof(NewUserActivity));
            }
        }

        private void GetViews()
        {
            ButtonLogin = FindViewById<Button>(Resource.Id.buttonLoginUser);
            ButtonNewUser = FindViewById<Button>(Resource.Id.buttonNewUser);
            EditTextLoginPassword = FindViewById<EditText>(Resource.Id.editTextLoginPassword);
            EditTextLoginUsername = FindViewById<EditText>(Resource.Id.editTextLoginUsername);
        }

        private void Localize()
        {
            ButtonLogin.Text = "Login".Translate();
            ButtonNewUser.Text = "New User".Translate();
            EditTextLoginPassword.Hint = "Password".Translate();
            EditTextLoginUsername.Hint = "Username".Translate();
        }

        private void SetStates()
        {
            ButtonLogin.Enabled = false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loginLayout);
         
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

            GetViews();
            SetStates();
            Localize();

            // Create your application here
            var localStore = AppStore.Instance;

            ButtonLogin.Click += (sender, args) =>
            {
                ViewModel.LoginCommand.Execute(null);
                ButtonLogin.Enabled = false;
                ButtonNewUser.Enabled = false;
            };

            ButtonNewUser.Click += (sender, args) =>
            {
                StartActivity(typeof(NewUserActivity));
            };

            EditTextLoginUsername.TextChanged += (sender, e) =>
            {
                ViewModel.Username = e.Text.ToString();
                ButtonLogin.Enabled = ViewModel.LoginCommand.CanExecute(null);
            };

            EditTextLoginPassword.TextChanged += (sender, e) =>
            {
                ViewModel.Password = e.Text.ToString();
                ButtonLogin.Enabled = ViewModel.LoginCommand.CanExecute(null);
            };

            //ToastMessage("Login Successful");
        }
    }
}
