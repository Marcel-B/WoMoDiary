using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class TripsViewModel : BaseViewModel
    {
        public ObservableCollection<Trip> Trips { get; set; }

        public TripsViewModel()
        {
            Trips = new ObservableCollection<Trip>();
        }

        public async Task PullTrips()
        {
            if (App.AllDataFetched)
            {
                // Get Trips from Users collection
                Trips.Clear();
                foreach (var trip in AppStore.Instance.User.Trips)
                    Trips.Add(trip);
                return;
            };
            var store = AppStore.Instance;
            var userId = store.User.Id;

            var trips = await TripStore.GetItemsByFkAsync(userId);
            if (trips == null) return;
            store.User.Trips = new System.Collections.Generic.List<Trip>();
            store.Trips.Clear();
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
