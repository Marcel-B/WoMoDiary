using Android.App;
using Android.OS;
using Android.Widget;

using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "NewUserActivity")]
    public class NewUserActivity : Activity
    {
        public NewUserActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewUserViewModel>();
            ViewModel.ErrorAction = ErrorMessage;
        }

        public NewUserViewModel ViewModel { get; set; }
        public EditText EditTextUsername { get; set; }
        public EditText EditTextEmail { get; set; }
        public EditText EditTextPassword { get; set; }
        public EditText EditTextConfirmPassword { get; set; }
        public Button ButtonNewUserSave { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.newUserLayout);
            GetControlls();
            Localize();
            SetControllEvents();
        }

        private void ErrorMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        private void GetControlls()
        {
            EditTextUsername = FindViewById<EditText>(Resource.Id.editTextNewUserUsername);
            EditTextEmail = FindViewById<EditText>(Resource.Id.editTextNewUserEmail);
            EditTextPassword = FindViewById<EditText>(Resource.Id.editTextNewUserPassword);
            EditTextConfirmPassword = FindViewById<EditText>(Resource.Id.editTextNewUserConfirmPassword);
            ButtonNewUserSave = FindViewById<Button>(Resource.Id.buttonNewUserSave);
        }

        private void Localize()
        {
            EditTextUsername.Hint = Strings.USERNAME;
            EditTextPassword.Hint = Strings.PASSWORD;
            EditTextConfirmPassword.Hint = Strings.CONFIRM_PASSWORD;
            ButtonNewUserSave.Text = Strings.SAVE_NEW_USER;
        }

        public void SetControllEvents()
        {
            EditTextUsername.TextChanged += (sender, e) =>
            {
                ViewModel.Username = e.Text.ToString();
                ButtonNewUserSave.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            EditTextEmail.TextChanged += (sender, e) =>
            {
                ViewModel.Email = e.Text.ToString();
                ButtonNewUserSave.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            EditTextPassword.TextChanged += (sender, e) =>
            {
                ViewModel.Password = e.Text.ToString();
                ButtonNewUserSave.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            EditTextConfirmPassword.TextChanged += (sender, e) =>
            {
                ViewModel.ConfirmPassword = e.Text.ToString();
                ButtonNewUserSave.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            ButtonNewUserSave.Click += (sender, e) =>
            {
                ViewModel.ConfirmNewUserCommand.Execute(null);
            };
        }
    }
}
