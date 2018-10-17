using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;
using WoMoDiary.Services;
using WoMoDiary;
using com.b_velop.WoMoDiary.Meta;
using System;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "WoMo Diary", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {

        public LoginActivity()
        {
            var app = new App();
            App.Initialize();
            ViewModel = ServiceLocator.Instance.Get<LoginViewModel>();
            ViewModel.LoginReady = IsReady;
            ViewModel.ErrorAction = ToastMessage;
        }

        public LoginViewModel ViewModel { get; set; }
        public Button ButtonLogin { get; set; }
        public Button ButtonNewUser { get; set; }
        public EditText EditTextLoginPassword { get; set; }
        public EditText EditTextLoginUsername { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loginLayout);

            GetViews();
            SetStates();
            SetControllEvents();
            Localize();
        }

        private void ToastMessage(string mssg)
            => Toast.MakeText(this, mssg, ToastLength.Long).Show();

        private void IsReady(bool isValid)
        {
            if (isValid)
            {
                StartActivity(typeof(TripActivity));
                Finish();
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
            ButtonLogin.Text = Strings.LOGIN;
            ButtonNewUser.Text = Strings.NEW_USER;
            EditTextLoginPassword.Hint = Strings.PASSWORD;
            EditTextLoginUsername.Hint = Strings.USERNAME;
        }

        private void SetStates()
        {
            ButtonLogin.Enabled = false;
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var str = prefs.GetString("UserGuid", "569DD649-F9F8-4990-B31B-45D43DDA82C2");
            var editor = prefs.Edit();
            editor.PutString("UserGuid", str);
            // editor.Commit();    // applies changes synchronously on older APIs
            editor.Apply();        // applies changes asynchronously on newer APIs
        }

        private void SetControllEvents()
        {
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
        }
    }
}
