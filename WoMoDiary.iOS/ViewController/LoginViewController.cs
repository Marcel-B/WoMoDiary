using com.b_velop.WoMoDiary.Meta;
using Foundation;
using System;
using UIKit;
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class LoginViewController : UIViewController
    {
        LoginViewModel ViewModel;
        public LoginViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<LoginViewModel>();
            ViewModel.LoginReady = LoginReady;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewModel.IsValid = false;
        }

        private void LoginReady(bool isValid)
        {
            if (isValid)
                BeginInvokeOnMainThread(() =>
                {
                    PerformSegue("ToTrip", this);
                });
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Localize();
            TextFieldUsername.EditingChanged += (sender, e) =>
            {
                if (sender is UITextField username)
                {
                    ViewModel.Username = username.Text;
                }
            };
            TextFieldPassword.EditingChanged += (sender, e) =>
            {
                if (sender is UITextField password)
                {
                    ViewModel.Password = password.Text;
                }
            };
            ButtonLogin.TouchUpInside += (sender, e) =>
            {
                ViewModel.LoginCommand.Execute(null);
                ButtonLogin.Enabled = false;
                ButtonNewUser.Enabled = false;
            };
        }

        private void Localize()
        {
            Title = Strings.LOGIN;
            TextFieldUsername.Placeholder = Strings.USERNAME;
            TextFieldPassword.Placeholder = Strings.PASSWORD;
            ButtonLogin.SetTitle(Strings.LOGIN, UIControlState.Normal);
            ButtonNewUser.SetTitle(Strings.NEW_USER, UIControlState.Normal);
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => segueIdentifier == "ToTrip" ? ViewModel.IsValid : true;
    }
}
