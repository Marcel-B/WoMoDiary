using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using System.Collections;
using System.Collections.Generic;

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

        public static void Initialize(string userId)
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
            PullData(Guid.Parse(userId));
            return;
        }

        public static async void PullData(Guid userId)
        {
            var userStore = ServiceLocator.Instance.Get<IDataStore<User>>();
            var localStore = AppStore.GetInstance();
            var user = userStore.GetItemAsync(userId).Result;
            if (user != null)
            {
                var tripStore = ServiceLocator.Instance.Get<IDataStore<Trip>>();
                var tmp = await tripStore.GetItemsByFkAsync(user.UserId);
                if (tmp == null) return;
                var trips = tmp.ToList();
                foreach (var trip in trips)
                {

                    //trip.User = localStore.User;
                    //var tmpPlace = clientOne.GetAsync($"https://womo.marcelbenders.de/api/trip/bytrip/{trip.TripId}").Result;
                    //var places = tmpPlace.Content.ReadAsStringAsync().Result;
                    //if (string.IsNullOrWhiteSpace(places)) continue;
                    //var pla = JsonConvert.DeserializeObject<IList<Place>>(places);
                    //trip.Places = pla.ToList();
                }

                localStore.User.Trips = trips;
            }
            //var currentUser = await userStore.GetItemAsync(userId);
            //localStore.User = currentUser;
            //var user = await userStore.GetItemAsync(App.User.Id);
            //foreach (var trip in trips)
            //{
            //    var places = await placeStore.GetItemsAsync(trip.Id, true);
            //    trip.Places = places.ToList();
            //}
            //localStore.Trips = currentUser.Trips.ToList();
            return;
        }
    }
}
