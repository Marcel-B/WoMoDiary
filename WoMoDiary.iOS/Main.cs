﻿using Foundation;
using UIKit;
using System;
using WoMoDiary.Services;
using WoMoDiary.Domain;

namespace WoMoDiary.iOS
{
    public class Application
    {

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            //var app = new App();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            // Get Shared User Defaults
            var plist = NSUserDefaults.StandardUserDefaults;

            // Save value
            if (plist["UserId"] == null)
                plist.SetString(Guid.NewGuid().ToString(), "UserId");

            if (App.UseMockDataStore)
                plist.SetString("569dd649-f9f8-4990-b31b-45d43dda82c2", "UserId");

            var userId = plist["UserId"].ToString();
            var localStore = AppStore.GetInstance();
            App.User = new User { Id = Guid.Parse(userId) };
            App.Initialize();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
