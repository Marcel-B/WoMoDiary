using System;
using System.Diagnostics;
using System.Reflection;
using I18NPortable;

namespace WoMoDiary
{
    public class App
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://localhost:5000";
        public App()
        {
            Console.WriteLine(GetType().GetTypeInfo().Assembly);
        }
        public static void Initialize()
        {
            if (UseMockDataStore)
                ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
            else
                ServiceLocator.Instance.Register<IDataStore<Item>, CloudDataStore>();


        }
    }
}
