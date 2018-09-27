using System;
using WoMoDiary.Domain;
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
            var store = ServiceLocator.Instance.Get<IDataStore<Trip>>();
            var trip = new Trip
            {
                Id = Guid.NewGuid(),
                Name = TripName,
                Description = Description,
                Created = DateTimeOffset.Now,
                User = App.User
            };
            localStore.Trips.Add(trip);
            await store.AddItemAsync(trip);
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
