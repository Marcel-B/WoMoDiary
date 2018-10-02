using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;

namespace WoMoDiary
{
    public class App
    {
        public static bool Init { get; set; }
        public const string USER_ID = "2c3facaef-14ef-4cc8-874f-0f9917082959";
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://womo.marcelbenders.de";
        public static Guid FirstTrip = Guid.NewGuid();
        public static Guid SecondTrip = Guid.NewGuid();
        public static User User { get; set; }

        public App()
        {
            Console.WriteLine(GetType().GetTypeInfo().Assembly);
        }

        public static async Task Initialize()
        {
            if (Init)
                return;
            Init = true;
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
            await PullData();
            return;
        }

        public static async Task PullData()
        {
            var userStore = ServiceLocator.Instance.Get<IDataStore<User>>();
            var localStore = AppStore.GetInstance();
            var foo = await userStore.UpdateItemAsync(App.User);
            var user = await userStore.GetItemAsync(App.User.Id);
            //foreach (var trip in trips)
            //{
            //    var places = await placeStore.GetItemsAsync(trip.Id, true);
            //    trip.Places = places.ToList();
            //}
            localStore.Trips = user.Trips.ToList();
            return;
        }
    }
}
