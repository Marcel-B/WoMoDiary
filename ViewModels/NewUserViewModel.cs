using System;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;

namespace WoMoDiary.ViewModels
{
    public class NewUserViewModel : BaseViewModel
    {
        public Command ConfirmNewUserCommand { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public NewUserViewModel()
        {
            ConfirmNewUserCommand = new Command(Execute, CanExecute);
        }

        private bool CanExecute(object arg)
               => !string.IsNullOrWhiteSpace(Password) &&
                  !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                  !string.IsNullOrWhiteSpace(Username) &&
                  !string.IsNullOrWhiteSpace(Email) &&
                  Password.Equals(ConfirmPassword);

        private async void Execute(object obj)
        {
            PasswordHelper.CreatePasswordHash(Password, out var hash, out var salt);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = Email,
                Hash = hash,
                Salt = salt,
                Created = DateTimeOffset.Now,
                LastEdit = DateTimeOffset.Now,
                Name = Username
            };
            var result = await UserStore.AddItemAsync(user);
            if (result == null)
                ErrorAction?.Invoke("Error to create new user.");
            else
                AppStore.Instance.User = user;
        }
    }
}
