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
    [Register ("MyPlacesMapViewController")]
    partial class MyPlacesMapViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapViewMyPlaces { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MapViewMyPlaces != null) {
                MapViewMyPlaces.Dispose ();
                MapViewMyPlaces = null;
            }
        }
    }
}