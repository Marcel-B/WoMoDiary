using System;
using UIKit;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;

namespace WoMoDiary.iOS
{
    public partial class NewUserViewController : UIViewController
    {
        public NewUserViewModel ViewModel { get; set; }

        public NewUserViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<NewUserViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
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