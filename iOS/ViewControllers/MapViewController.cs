using System;
using UIKit;
using WoMoDiary.iOS.HelpersIOS;
using System.Runtime.CompilerServices;
using CoreLocation;
using MapKit;
using WoMoDiary.Helpers;
using I18NPortable;

namespace WoMoDiary.iOS
{
    public partial class MapViewController : UIViewController
    {
        public static LocationManager Manager { get; set; }
   
        public MapViewController (IntPtr handle) : base (handle)
        {
            Manager = new LocationManager();
            Manager.StartLocationUpdates();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = Strings.YOUR_POSITION.Translate();
            ButtonSave.SetTitle(Strings.SAVE_POSITION.Translate(), UIControlState.Normal);
            WoMoMap.ShowsBuildings = true;
            WoMoMap.SetUserTrackingMode(MapKit.MKUserTrackingMode.Follow, true);

            // It is better to handle this with notifications, so that the UI updates
            // resume when the application re-enters the foreground!
            //Manager.LocationUpdated += HandleLocationChanged;

            // Screen subscribes to the location changed event
            UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
            {
                Manager.LocationUpdated += HandleLocationChanged;
            });

            // Whenever the app enters the background state, we unsubscribe from the event 
            // so we no longer perform foreground updates
            UIApplication.Notifications.ObserveDidEnterBackground((sender, args) =>
            {
                Manager.LocationUpdated -= HandleLocationChanged;
            });
        }

        public void HandleLocationChanged(object sender, LocationUpdatedEventArgs e)
        {
            // Handle foreground updates
            CLLocation location = e.Location;
            Title = location.Speed.ToString();
            Console.WriteLine("foreground updated");

            var target = WoMoMap.UserLocation.Location.Coordinate;
            var viewPoint = target;
            var camera = MKMapCamera.CameraLookingAtCenterCoordinate(target, viewPoint, 500);
            WoMoMap.Camera = camera;
        }
    }
}