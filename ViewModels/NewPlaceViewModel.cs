using System;
using System.Collections.Generic;
using System.Text;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using Android.Util;

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
                    tmp = new CampingPlace
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Description = Description,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Altitude = Altitude,
                        Rating = Rating,
                        Created = DateTimeOffset.Now
                    };
                    break;
                case PlaceType.Hotel:
                    tmp = new Hotel
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Description = Description,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Altitude = Altitude,
                        Rating = Rating,
                        Created = DateTimeOffset.Now
                    };
                    break;
                case PlaceType.MotorhomePlace:
                    tmp = new Stellpatz
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Description = Description,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Altitude = Altitude,
                        Rating = Rating,
                        Created = DateTimeOffset.Now
                    };
                    break;
                case PlaceType.Restaurant:
                    tmp = new Restaurant
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Description = Description,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Altitude = Altitude,
                        Rating = Rating,
                        Created = DateTimeOffset.Now
                    };
                    break;
                case PlaceType.SightSeeing:
                    tmp = new NicePlace
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Description = Description,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Altitude = Altitude,
                        Rating = Rating,
                        Created = DateTimeOffset.Now
                    };
                    break;

            }
            store.CurrentTrip.Places.Add(tmp);
            var pl = ServiceLocator.Instance.Get<IDataStore<Place>>();
            await pl.AddItemAsync(tmp);
        }
    }
}
