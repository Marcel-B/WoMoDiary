using System.Reflection;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;

namespace WoMoDiary
{
    public class App
    {
        public static bool Init { get; set; }
        public static bool AllDataFetched { get; set; }
        public static bool UseMockDataStore = false;
        public const string BACKEND_URL = "https://womo.marcelbenders.de";

        //public static User User { get; set; }

        public App()
        {
            AllDataFetched = false;
            Init = false;
            System.Diagnostics.Debug.WriteLine(GetType().GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Initialization of IOC container
        /// </summary>
        public static void Initialize()
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
            ServiceLocator.Instance.Register<PlacesViewModel, PlacesViewModel>();
            ServiceLocator.Instance.Register<TripsViewModel, TripsViewModel>();
            ServiceLocator.Instance.Register<NewUserViewModel, NewUserViewModel>();
            ServiceLocator.Instance.Register<NewPlaceViewModel, NewPlaceViewModel>();
            ServiceLocator.Instance.Register<LoginViewModel, LoginViewModel>();
            ServiceLocator.Instance.Register<NewTripViewModel, NewTripViewModel>();
            return;
        }
    }
}
