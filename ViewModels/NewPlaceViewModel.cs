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
            var store = AppStore.GetInstance();
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
            store.User.Trips.Single(t => t.TripId == store.CurrentTrip.TripId).Places.Add(tmp);
            var tripStore = ServiceLocator.Instance.Get<IDataStore<Place>>();
            //await tripStore.Add(tmp);
            //store.CurrentTrip.Places.Add(tmp);
            //var pl = ServiceLocator.Instance.Get<IDataStore<Place>>();
            //await pl.AddItemAsync(tmp);
            //var tripStore = ServiceLocator.Instance.Get<IDataStore<Trip>>();
            //await tripStore.UpdateItemAsync(store.CurrentTrip);
        }
    }
}
