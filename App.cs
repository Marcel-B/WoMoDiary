using System;
using System.Reflection;
using I18NPortable;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;

namespace WoMoDiary
{
    public class App
    {
        public static bool UseMockDataStore = false;
        public static bool Init { get; set; }
        public static bool AllDataFetched { get; set; }
        public const string BACKEND_URL = "https://womo.marcelbenders.de";

        public App()
        {
            AllDataFetched = false;
            Init = false;
            App.LogOutLn(GetType().GetTypeInfo().Assembly); 
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

            I18N.Current
                .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
                .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                .SetLogger(text => App.LogOutLn(text)) // action to output traces
                .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                .Init(typeof(App).GetTypeInfo().Assembly); // assembly where locales live

            return;
        }

        public static void LogOutLn(object mssg)
            => System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] - {mssg}");
    }
}
