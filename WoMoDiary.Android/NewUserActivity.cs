
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoMoDiary.ViewModels;
using Java.Util;
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
        }

  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.newUserLayout);

            var username = FindViewById<EditText>(Resource.Id.editTextNewUserUsername);
            var password = FindViewById<EditText>(Resource.Id.editTextNewUserPassword);
            var confirmPassword = FindViewById<EditText>(Resource.Id.editTextNewUserConfirmPassword);
            var button = FindViewById<Button>(Resource.Id.buttonNewUserSave);

            username.TextChanged += (sender, e) =>
            {
                ViewModel.Username = e.Text.ToString();
            };

            password.TextChanged += (sender, e) =>
            {
                ViewModel.Password = e.Text.ToString();
            };

            confirmPassword.TextChanged += (sender, e) =>
            {
                ViewModel.ConfirmPassword = e.Text.ToString();
            };
            button.Click += (sender, e) =>
            {
                ViewModel.ConfirmNewUserCommand.Execute(null);
            };
        }
    }
}
