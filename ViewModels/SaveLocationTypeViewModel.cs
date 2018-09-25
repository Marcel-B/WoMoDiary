using System;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using System.Collections.Generic;

namespace WoMoDiary.ViewModels
{
    public class SaveLocationTypeViewModel : BaseViewModel
    {
        public Place Place { get; set; }
        public Command SaveLocationTypeCommand { get; set; }

        public SaveLocationTypeViewModel()
        {
            SaveLocationTypeCommand = new Command(Execute, CanExecute);
        }

        private bool CanExecute(object arg)
            => true;

        private async void Execute(object obj)
        {
            var cloud = ServiceLocator.Instance.Get<IDataStore<Place>>();
            var store = AppStore.GetInstance();
            var place = store.CurrentPlace;
            var trip = store.CurrentTrip;
            Place.Id = Guid.NewGuid();
            Place.Name = place.Name;
            Place.Description = place.Description;
            Place.TripFk = trip.Id;
            Place.Longitude = place.Longitude;
            Place.Latitude = place.Latitude;
            Place.Altitude = place.Altitude;
            trip.Places.Add(Place);
            await cloud.AddItemAsync(Place);
            System.Diagnostics.Debug.WriteLine("Location saved");
        }
    }
}
