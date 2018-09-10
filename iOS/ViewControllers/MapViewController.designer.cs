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

namespace WoMoDiary.iOS
{
    [Register ("MapViewController")]
    partial class MapViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView WoMoMap { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSave != null) {
                ButtonSave.Dispose ();
                ButtonSave = null;
            }

            if (WoMoMap != null) {
                WoMoMap.Dispose ();
                WoMoMap = null;
            }
        }
    }
}