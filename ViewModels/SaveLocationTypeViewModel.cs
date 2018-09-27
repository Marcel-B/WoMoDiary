using System;
using WoMoDiary.Domain;
using WoMoDiary.Services;

namespace WoMoDiary.ViewModels
{
    public class SaveLocationTypeViewModel : BaseViewModel
    {
        public PlaceType PlaceType { get; set; }
        public short Rating { get; set; }

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
            place.Type = PlaceType;
            switch (PlaceType)
            {
                case PlaceType.CampingPlace:
                    place.AssetName = "Camping";
                    break;
                case PlaceType.SightSeeing:
                    place.AssetName = "SightSeeing";
                    break;
                case PlaceType.Hotel:
                    place.AssetName = "Hotel";
                    break;
                case PlaceType.Restaurant:
                    place.AssetName = "Restaurant";
                    break;
                case PlaceType.MotorhomePlace:
                    place.AssetName = "Camping";
                    break;
                default:
                    place.AssetName = "Default";
                    break;
            }
            place.Rating = Rating;
            place.Trip = trip;
            trip.Places.Add(place);
            
            var result = await cloud.AddItemAsync(place);
            System.Diagnostics.Debug.WriteLine($"Location'{place.Name}' saved: {result}");
        }
    }
}
