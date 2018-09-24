using System;
using System.Diagnostics;
using System.Reflection;
using I18NPortable;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    public class App
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://womo.marcelbenders.de";
        public App()
        {
            Console.WriteLine(GetType().GetTypeInfo().Assembly);
        }
        public static void Initialize()
        {
            //if (UseMockDataStore)
            //    ServiceLocator.Instance.Register<IDataStore<Trip>, MockDataStore>();
            //else
                //ServiceLocator.Instance.Register<IDataStore<Trip>, CloudDataStore>();
        }
    }
}
