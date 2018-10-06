using System;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;

namespace WoMoDiary.ViewModels
{
    public class NewTripViewModel : BaseViewModel
    {
        public NewTripViewModel()
        {
            SaveTripCommand = new Command(ExecuteSaveTrip, CanExecuteSaveTrip);
        }

        private bool CanExecuteSaveTrip(object arg)
            => !string.IsNullOrWhiteSpace(TripName);

        private async void ExecuteSaveTrip(object obj)
        {
            var localStore = AppStore.GetInstance();
            var trip = new Trip
            {
                TripId = Guid.NewGuid(),
                Name = TripName,
                Description = Description,
                Created = DateTimeOffset.Now,
                User = localStore.User,
                UserId = localStore.User.Id
            };
            localStore.User.Trips.Add(trip);
            localStore.Trips.Add(trip);
            var result = await TripStore.AddItemAsync(trip);
        }

        private string _tripName;
        public string TripName
        {
            get => _tripName;
            set => SetProperty(ref _tripName, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public Command SaveTripCommand { get; set; }

    }
}
