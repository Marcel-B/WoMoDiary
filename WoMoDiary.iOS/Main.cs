using Foundation;
using UIKit;
using System;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using System.Threading.Tasks;

namespace WoMoDiary.iOS
{
    public class Application
    {

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            var plist = NSUserDefaults.StandardUserDefaults;

            // Save value
            if (plist["UserId"] == null)
                plist.SetString(Guid.NewGuid().ToString(), "UserId");

            if (App.UseMockDataStore)
                plist.SetString("569dd649-f9f8-4990-b31b-45d43dda82c2", "UserId");

            var userId = plist["UserId"].ToString();
            var localStore = AppStore.Instance;
            App.Initialize();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
