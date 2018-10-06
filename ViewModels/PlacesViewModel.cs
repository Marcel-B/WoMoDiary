using System;
using System.Collections.ObjectModel;
using WoMoDiary.Domain;
using System.Threading.Tasks;
using WoMoDiary.Services;
using System.Linq;

namespace WoMoDiary.ViewModels
{
    public class PlacesViewModel : BaseViewModel
    {
        public ObservableCollection<Place> Places { get; set; }

        public PlacesViewModel()
        {
            if (Places == null)
                Places = new ObservableCollection<Place>();
        }

        public void PullPlaces()
        {
            Places.Clear();
            var store = AppStore.GetInstance();
            var trip = store.CurrentTrip;
            foreach (var place in trip.Places)
            {
                Places.Add(place);
            }
            return;
        }

        public async Task PullPlaces(Guid tripId)
        {
            Places.Clear();
            var places = await PlaceStore.GetItemsByFkAsync(tripId);
            foreach (var place in places)
            {
                Places.Add(place);
            }
            var store = AppStore.GetInstance();
            store.User.Trips
                 .Single(t => t.Id == tripId)
                 .Places = places.ToList();
            return;
        }
    }
}
