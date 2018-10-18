using Foundation;
using UIKit;
using System;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.iOS
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

            if (plist["Username"] != null)
                App.LogOutLn($"We have an User named {plist["Username"]}", typeof(Application).Name);
            else
                App.LogOutLn($"No User", typeof(Application).Name);

            if (App.UseMockDataStore)
                plist.SetString("569dd649-f9f8-4990-b31b-45d43dda82c2", "UserId");

            var userId = plist["UserId"].ToString();
            var localStore = AppStore.Instance;
            App.Initialize();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
