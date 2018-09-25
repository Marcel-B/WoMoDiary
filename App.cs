using System;
using System.Reflection;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    public class App
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://womo.marcelbenders.de";
        public App()
        {
            Console.WriteLine(GetType().GetTypeInfo().Assembly);
        }
        public static void Initialize()
        {
            if (UseMockDataStore)
                ServiceLocator.Instance.Register<IDataStore<TripOtd>, MockDataStore>();
            else
                ServiceLocator.Instance.Register<IDataStore<TripOtd>, CloudDataStore>();
        }
    }
}
