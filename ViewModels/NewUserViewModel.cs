﻿using System;
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
            => Password.Equals(ConfirmPassword) && !string.IsNullOrWhiteSpace(Email);

        private async void Execute(object obj)
        {
            var id = AppStore.GetInstance().UserId;
            PasswordHelper.CreatePasswordHash(Password, out var hash, out var salt);
            var user = new User
            {
                Id = id,
                Email = Email,
                Hash = hash,
                Salt = salt,
                Created = DateTimeOffset.Now,
                LastEdit = DateTimeOffset.Now,
                Name = Username
            };
            var result = await UserStore.AddItemAsync(user);
        }
    }
}
