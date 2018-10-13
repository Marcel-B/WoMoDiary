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
using WoMoDiary.ViewModels;

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
