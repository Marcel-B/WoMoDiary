using System;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using System.Linq;

namespace WoMoDiary.ViewModels
{
    public class NewPlaceViewModel : BaseViewModel
    {
        public PlaceType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public Command SavePlaceCommand { get; set; }

        public NewPlaceViewModel()
        {
            SavePlaceCommand = new Command(ExecuteSave, CanExecuteSave);
        }

        private bool CanExecuteSave(object arg)
            => true;

        private async void ExecuteSave(object obj)
        {
            var store = AppStore.Instance;
            Place tmp = null;
            switch (Type)
            {
                case PlaceType.CampingPlace:
                    tmp = new CampingPlace();
                    break;
                case PlaceType.Hotel:
                    tmp = new Hotel();
                    break;
                case PlaceType.MotorhomePlace:
                    tmp = new Stellpatz();
                    break;
                case PlaceType.Restaurant:
                    tmp = new Restaurant();
                    break;
                case PlaceType.SightSeeing:
                    tmp = new NicePlace();
                    break;
            }

            tmp.Latitude = Latitude;
            tmp.Longitude = Longitude;
            tmp.Rating = Rating;
            tmp.Altitude = Altitude;
            tmp.Created = DateTimeOffset.Now;
            tmp.Description = Description;
            tmp.Name = Name;
            tmp.PlaceId = Guid.NewGuid();
            tmp.Trip = store.CurrentTrip;
            tmp.TripId = store.CurrentTrip.TripId;

            var trips = store.User.Trips;
            var tripIdx = trips.IndexOf(store.CurrentTrip);

            // Add new Place to local Collection
            store.User.Trips[tripIdx].Places.Add(tmp);

            // Save to CloudStore
            await PlaceStore.AddItemAsync(tmp);
        }
    }
}
