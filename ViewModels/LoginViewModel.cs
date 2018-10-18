using System;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object arg)
            => !(string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password));

        private async void ExecuteLogin(object obj)
        {
            await UserStore
                .GetByName(Username)
                .ContinueWith(HandleAction);
        }

        private void HandleAction(System.Threading.Tasks.Task<Domain.User> obj)
        {
            var user = obj.Result;
            if (user == null)
                IsValid = false;
            else
                IsValid = PasswordHelper.VerifyPasswordHash(Password, user.Hash, user.Salt);

            if (IsValid)
            {
                AppStore.Instance.User = user;
            }
            LoginReadyCallback?.Invoke(IsValid);
        }

        public Action<bool> LoginReadyCallback { get; set; }

        private bool _isValid;

        public bool IsValid { get => _isValid; set => SetProperty(ref _isValid, value); }

        public string Username { get; set; }

        public string Password { get; set; }

        public Command LoginCommand { get; set; }
    }
}
