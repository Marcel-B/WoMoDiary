using System;
using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class NewUserViewModel : BaseViewModel
    {
        public NewUserViewModel()
        {
            ConfirmNewUserCommand = new Command(Execute, CanExecute);
        }

        public Command ConfirmNewUserCommand { get; set; }
        public Action<bool> NewUserSucceeded { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        private bool CanExecute(object arg)
               => !string.IsNullOrWhiteSpace(Password) &&
                  !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                  !string.IsNullOrWhiteSpace(Username) &&
                  !string.IsNullOrWhiteSpace(Email) &&
                  Password.Equals(ConfirmPassword);

        private async void Execute(object obj)
        {
            try
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
                {
                    ErrorAction?.Invoke(Strings.ERROR_OCCURED_WHILE_CREATING_NEW_USER);
                    NewUserSucceeded?.Invoke(false);
                }
                else
                {
                    AppStore.Instance.User = user;
                    NewUserSucceeded?.Invoke(true);
                }
            }
            catch (Exception ex)
            {
                ErrorAction?.Invoke(Strings.ERROR_OCCURED_WHILE_CREATING_NEW_USER);
                App.LogOutLn(ex.Message, GetType().Name);
                NewUserSucceeded?.Invoke(false);
            }
        }
    }
}
