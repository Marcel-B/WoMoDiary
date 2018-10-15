using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using System.Linq;

namespace WoMoDiary.ViewModels
{
    public class TripsViewModel : BaseViewModel
    {
        public ObservableCollection<Trip> Trips { get; set; }

        public TripsViewModel()
        {
            if (Trips == null)
            {
                Trips = new ObservableCollection<Trip>();
                Task.Run(PullTrips);
            }
        }

        public async Task PullTrips()
        {
            if (App.AllDataFetched) return;
            var store = AppStore.GetInstance();
            var userId = store.User.Id;

            var trips = await TripStore.GetItemsByFkAsync(userId);
            if (trips == null) return;
            store.User.Trips = new System.Collections.Generic.List<Trip>();
            foreach (var trip in trips)
            {
                var places = await PlaceStore.GetItemsByFkAsync(trip.Id);
                trip.Places = places.ToList();
                store.User.Trips.Add(trip);
                Trips.Add(trip);
            }

            // All data for this User are fetched yet
            App.AllDataFetched = true;
            return;
        }
    }
}
