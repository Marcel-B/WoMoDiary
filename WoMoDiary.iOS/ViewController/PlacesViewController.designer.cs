// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    [Register ("TripsViewController")]
    partial class PlacesViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapViewPlaces { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MapViewPlaces != null) {
                MapViewPlaces.Dispose ();
                MapViewPlaces = null;
            }
        }
    }
}