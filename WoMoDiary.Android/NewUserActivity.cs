﻿
using Android.App;
using Android.OS;
using Android.Widget;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;

namespace WoMoDiary
{
    [Activity(Label = "NewUserActivity")]
    public class NewUserActivity : Activity
    {
        public NewUserViewModel ViewModel { get; set; }

        public NewUserActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewUserViewModel>();
            ViewModel.ErrorAction = ErrorMessage;
        }

        private void ErrorMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.newUserLayout);

            var username = FindViewById<EditText>(Resource.Id.editTextNewUserUsername);
            var email = FindViewById<EditText>(Resource.Id.editTextNewUserEmail);
            var password = FindViewById<EditText>(Resource.Id.editTextNewUserPassword);
            var confirmPassword = FindViewById<EditText>(Resource.Id.editTextNewUserConfirmPassword);
            var button = FindViewById<Button>(Resource.Id.buttonNewUserSave);

            username.TextChanged += (sender, e) =>
            {
                ViewModel.Username = e.Text.ToString();
                button.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            email.TextChanged += (sender, e) =>
            {
                ViewModel.Email = e.Text.ToString();
                button.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            password.TextChanged += (sender, e) =>
            {
                ViewModel.Password = e.Text.ToString();
                button.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            confirmPassword.TextChanged += (sender, e) =>
            {
                ViewModel.ConfirmPassword = e.Text.ToString();
                button.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
            };
            button.Click += (sender, e) =>
            {
                ViewModel.ConfirmNewUserCommand.Execute(null);
            };
        }
    }
}
