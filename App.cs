using System;
using System.Reflection;
using I18NPortable;

using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary
{
    public class App
    {

#if DEBUG
        public static bool UseMockDataStore = true;
#else
        public static bool UseMockDataStore = false;
#endif

        public static bool Init { get; set; }
        public static bool AllDataFetched { get; set; }
        // ReSharper disable once InconsistentNaming
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
            ServiceLocator.Instance.Register<PlaceDetailViewModel, PlaceDetailViewModel>();
            ServiceLocator.Instance.Register<EditPlaceViewModel, EditPlaceViewModel>();

            LogOutLn("Logger test", typeof(App).Name);

            I18N.Current
                .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
                .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                .SetLogger(text => App.LogOutLn(text.Substring(7), typeof(I18N).Name)) // action to output traces
                .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                .Init(typeof(App).GetTypeInfo().Assembly); // assembly where locales live

            return;
        }

        public static void LogOutLn(object mssg, object sender = null)
        {
            if (sender == null)
                System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] - {mssg}");
            else
            {
                var ob = $"[{sender}]";
                ob = ob.PadRight(16);
                System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] - {ob} {mssg}");
            }
        }
    }
}
