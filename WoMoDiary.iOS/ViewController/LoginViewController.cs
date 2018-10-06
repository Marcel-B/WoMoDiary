using Foundation;
using System;
using UIKit;
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;
using WoMoDiary.Services;

namespace WoMoDiary.iOS
{
    public partial class LoginViewController : UIViewController
    {
        LoginViewModel ViewModel;
        public LoginViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<LoginViewModel>();
            byte[] passwordHash;
            byte[] saltHash;
            string password = "Hy";
            PasswordHelper.CreatePasswordHash(password, out passwordHash, out saltHash);

        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TextFieldUsername.Text = AppStore.GetInstance().UserId.ToString();
            TextFieldPassword.Text = "a";
            ViewModel.Username = AppStore.GetInstance().UserId.ToString();
            ViewModel.Password = "a";
            ViewModel.IsValid = false;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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
            };
            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (sender is LoginViewModel viewModel)
                {
                    if (e.PropertyName == "IsValid")
                    {
                        if (viewModel.IsValid)
                        {
                            BeginInvokeOnMainThread(() =>
                            {
                                PerformSegue("ToTrip", this);
                            });
                        }
                    }
                }
            };
        }
        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => segueIdentifier == "ToTrip" ? ViewModel.IsValid : true;
    }
}