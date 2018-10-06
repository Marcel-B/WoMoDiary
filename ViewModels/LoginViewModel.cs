using System;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
namespace WoMoDiary.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object arg)
        => true;//!(string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password));

        private async void ExecuteLogin(object obj)
        {
            var user = await UserStore.GetItemAsync(AppStore.GetInstance().UserId);
            IsValid = PasswordHelper.VerifyPasswordHash(Password, user.Hash, user.Salt);
        }

        private bool _isValid;
        public bool IsValid { get => _isValid; set => SetProperty(ref _isValid, value); }

        public string Username { get; set; }

        public string Password { get; set; }

        public Command LoginCommand { get; set; }

    }
}
