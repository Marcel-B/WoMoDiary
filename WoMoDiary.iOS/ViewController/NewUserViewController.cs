using System;
using UIKit;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.iOS
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
            ButtonConfirm.SetTitle(Strings.SAVE_NEW_USER, UIControlState.Normal);
            TextFieldPassword.Placeholder = Strings.PASSWORD;
            TextFieldUsername.Placeholder = Strings.USERNAME;
            TextFieldConfirmPassword.Placeholder = Strings.CONFIRM_PASSWORD;
            Title = Strings.NEW_USER;
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