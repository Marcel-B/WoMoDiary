using System;
using UIKit;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using I18NPortable;

namespace WoMoDiary.iOS
{
    public partial class NewUserViewController : UIViewController
    {
        public NewUserViewModel ViewModel { get; set; }

        public NewUserViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<NewUserViewModel>();
        }

        private void Localize()
        {
            ButtonConfirm.SetTitle("Save new user".Translate(), UIControlState.Normal);
            TextFieldPassword.Placeholder = "Password".Translate();
            TextFieldUsername.Placeholder = "Username".Translate();
            TextFieldConfirmPassword.Placeholder = "Confirm Password".Translate();
            Title = "New User".Translate();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Localize();
            ButtonConfirm.TouchUpInside += (sender, e) =>
            {
                ViewModel.ConfirmNewUserCommand.Execute(null);
            };

            TextFieldEmail.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.Email = textView.Text;
                }
            };

            TextFieldPassword.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.Password = textView.Text;
                }
            };

            TextFieldUsername.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.Username = textView.Text;
                }
            };

            TextFieldConfirmPassword.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.ConfirmPassword = textView.Text;
                }
            };
        }
    }
}