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
                Task.Run(PullData);
            }
        }

        public async Task PullData()
        {
            var store = AppStore.GetInstance();
            var userId = store.UserId;
            var userStore = ServiceLocator.Instance.Get<IDataStore<User>>();
            var user = await userStore.GetItemAsync(userId);
            if (user == null) return;
            store.User = user;
            var tripStore = ServiceLocator.Instance.Get<IDataStore<Trip>>();
            var trips = await tripStore.GetItemsByFkAsync(userId);
            if (trips == null) return;
            store.User.Trips = new System.Collections.Generic.List<Trip>();
            foreach (var trip in trips)
            {
                var places = await PlaceStore.GetItemsByFkAsync(trip.Id);
                trip.Places = places.ToList();
                store.User.Trips.Add(trip);
                Trips.Add(trip);
            }

            return;
        }
    }
}
