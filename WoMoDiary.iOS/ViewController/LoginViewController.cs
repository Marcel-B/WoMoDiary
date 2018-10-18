using Foundation;
using System;
using UIKit;

using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<LoginViewModel>();
            ViewModel.LoginReadyCallback = LoginReady;
        }

        public LoginViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Localize();
            SetStates();
            SetControllEvents();
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => segueIdentifier != "ToTrip" || ViewModel.IsValid;

        private void LoginReady(bool isValid)
        {
            if (isValid)
                BeginInvokeOnMainThread(() =>
                {
                    PerformSegue("ToTrip", this);
                });
            else
            {
                ButtonLogin.Enabled = true;
            }
        }

        private void Localize()
        {
            Title = Strings.LOGIN;
            TextFieldUsername.Placeholder = Strings.USERNAME;
            TextFieldPassword.Placeholder = Strings.PASSWORD;
            ButtonLogin.SetTitle(Strings.LOGIN, UIControlState.Normal);
            ButtonNewUser.SetTitle(Strings.NEW_USER, UIControlState.Normal);
        }

        private void SetStates()
        {
            ViewModel.IsValid = false;
        }

        private void SetControllEvents()
        {
            TextFieldUsername.EditingChanged += (sender, e) =>
            {
                if (sender is UITextField username)
                {
                    ViewModel.Username = username.Text;
                    ButtonLogin.Enabled = ViewModel.LoginCommand.CanExecute(null);
                }
            };
            TextFieldPassword.EditingChanged += (sender, e) =>
            {
                if (sender is UITextField password)
                {
                    ViewModel.Password = password.Text;
                    ButtonLogin.Enabled = ViewModel.LoginCommand.CanExecute(null);
                }
            };
            ButtonLogin.TouchUpInside += (sender, e) =>
            {
                ViewModel.LoginCommand.Execute(null);
                ButtonLogin.Enabled = false;
                ButtonNewUser.Enabled = false;
            };
        }
    }
}
