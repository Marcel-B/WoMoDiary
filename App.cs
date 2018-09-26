using System;
using System.Reflection;
using WoMoDiary.Domain;
using WoMoDiary.Services;

namespace WoMoDiary
{
    public class App
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://womo.marcelbenders.de";
        public static Guid FirstTrip = Guid.NewGuid();
        public static Guid SecondTrip = Guid.NewGuid();
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
            }
            else
            {
                ServiceLocator.Instance.Register<IDataStore<Trip>, TripDataStore>();
                ServiceLocator.Instance.Register<IDataStore<Place>, PlaceDataStore>();
            }
        }
    }
}
