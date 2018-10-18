using System;
using UIKit;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class NewUserViewController : UIViewController
    {

        public NewUserViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<NewUserViewModel>();
            ViewModel.NewUserSucceeded = NewUserSucceeded;
            ViewModel.ErrorAction = ShowMessage;
        }

        public NewUserViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetStates();
            Localize();
            SetControllEvents();
        }

        private void SetStates()
        {
            ButtonConfirm.Enabled = false;
        }

        private void SwitchStates(bool newState)
        {
            TextFieldEmail.Enabled = newState;
            TextFieldPassword.Enabled = newState;
            TextFieldUsername.Enabled = newState;
            TextFieldConfirmPassword.Enabled = newState;
            ButtonConfirm.Enabled = newState;
        }

        private void NewUserSucceeded(bool success)
        {
            if (success)
            {
                PerformSegue("FromNewUserToTrips", this);
            }
            else
            {
                SwitchStates(true);
            }
        }

        private void ShowMessage(string message)
        {

        }

        private void Localize()
        {
            ButtonConfirm.SetTitle(Strings.SAVE_NEW_USER, UIControlState.Normal);
            TextFieldPassword.Placeholder = Strings.PASSWORD;
            TextFieldUsername.Placeholder = Strings.USERNAME;
            TextFieldConfirmPassword.Placeholder = Strings.CONFIRM_PASSWORD;
            Title = Strings.NEW_USER;
        }

        private void SetControllEvents()
        {
            ButtonConfirm.TouchUpInside += (sender, e) =>
            {
                SwitchStates(false);
                ViewModel.ConfirmNewUserCommand.Execute(null);
            };

            TextFieldEmail.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.Email = textView.Text;
                    ButtonConfirm.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
                }
            };

            TextFieldPassword.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.Password = textView.Text;
                    ButtonConfirm.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
                }
            };

            TextFieldUsername.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.Username = textView.Text;
                    ButtonConfirm.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
                }
            };

            TextFieldConfirmPassword.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textView)
                {
                    ViewModel.ConfirmPassword = textView.Text;
                    ButtonConfirm.Enabled = ViewModel.ConfirmNewUserCommand.CanExecute(null);
                }
            };
        }
    }
}