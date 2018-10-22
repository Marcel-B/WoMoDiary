using System;

using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class NewTripViewModel : BaseViewModel
    {
        public NewTripViewModel()
        {
            SaveTripCommand = new Command(ExecuteSaveTrip, CanExecuteSaveTrip);
        }

        /// <summary>
        /// Cans the execute save trip.
        /// </summary>
        /// <returns><c>true</c>, if execute save trip was caned, <c>false</c> otherwise.</returns>
        /// <param name="arg">Argument.</param>
        private bool CanExecuteSaveTrip(object arg)
            => !string.IsNullOrWhiteSpace(TripName);

        private async void ExecuteSaveTrip(object obj)
        {
            var localStore = AppStore.Instance;
            var trip = new Trip
            {
                TripId = Guid.NewGuid(),
                Name = TripName,
                Description = Description,
                Created = DateTimeOffset.Now,
                LastEdit = DateTimeOffset.Now,
                User = localStore.User,
                UserId = localStore.User.Id
            };

            // Local update
            localStore.User.Trips.Add(trip);

            // Cloud update
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
