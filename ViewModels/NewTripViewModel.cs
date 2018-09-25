using System;
using WoMoDiary.Domain;
using Network;

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
            var store = MockDataStore.GetInstance();
            var cloud = new TripDataStore();

            var trip = new TripOtd
            {
                Id = Guid.NewGuid(),
                Name = TripName,
                Description = Description
            };
            await cloud.AddItemAsync(trip);
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
