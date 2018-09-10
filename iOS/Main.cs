using System;
using System.Diagnostics;
using Foundation;
using I18NPortable;
using UIKit;

namespace WoMoDiary.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            //NSUserDefaults.StandardUserDefaults.SetValueForKey(NSArray.FromStrings("de"), new NSString("AppleLanguages"));
            //NSUserDefaults.StandardUserDefaults.Synchronize();
            //var lang = NSUserDefaults.StandardUserDefaults;
            var app = new App();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
