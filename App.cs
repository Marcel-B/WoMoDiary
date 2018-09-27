using System;
using System.Reflection;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using System.Linq;

namespace WoMoDiary
{
    public class App
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://womo.marcelbenders.de";
        public static Guid FirstTrip = Guid.NewGuid();
        public static Guid SecondTrip = Guid.NewGuid();
        public static Guid UserId { get; set; }

        public App()
        {
            Console.WriteLine(GetType().GetTypeInfo().Assembly);
        }

        public static void Initialize()
        {
            if (UseMockDataStore)
            {

                ServiceLocator.Instance.Register<IDataStore<Trip>, MockTripDataStore>();
                ServiceLocator.Instance.Register<IDataStore<Place>, MockPlaceDataStore>();
                ServiceLocator.Instance.Register<IDataStore<User>, MockUserDataStore>();
            }
            else
            {
                ServiceLocator.Instance.Register<IDataStore<Trip>, TripDataStore>();
                ServiceLocator.Instance.Register<IDataStore<Place>, PlaceDataStore>();
                ServiceLocator.Instance.Register<IDataStore<User>, UserDataStore>();
            }
            var store = AppStore.GetInstance();
            PullData();
        }

        public async static void PullData()
        {
            var store = ServiceLocator.Instance.Get<IDataStore<Trip>>();
            var placeStore = ServiceLocator.Instance.Get<IDataStore<Place>>();
            var localStore = AppStore.GetInstance();

            var trips = await store.GetItemsAsync(localStore.UserId, true);
            foreach (var trip in trips)
            {
                var places = await placeStore.GetItemsAsync(trip.Id, true);
                trip.Places = places.ToList();
            }
            localStore.Trips = trips.ToList();
        }
    }
}
