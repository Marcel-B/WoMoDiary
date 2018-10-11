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
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;

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

            // Create your application here
            SetContentView(Resource.Layout.loginLayout);

            var username = FindViewById<EditText>(Resource.Id.editTextLoginUsername);
            username.TextChanged += (sender, args) => { ViewModel.Username = username.Text; };

            var password = FindViewById<EditText>(Resource.Id.editTextLoginPassword);
            password.TextChanged += (sender, args) => { ViewModel.Password = password.Text; };

            var button = FindViewById<Button>(Resource.Id.buttonLoginUser);
            button.Click += (sender, args) =>
            {
                ViewModel.LoginCommand.Execute(null);
            };


        }
    }
}