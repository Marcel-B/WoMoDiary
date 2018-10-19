// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace com.b_velop.WoMoDiary.iOS
{
    [Register ("MapViewController")]
    partial class NewPlaceMapViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSavePosition { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView Map { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSavePosition != null) {
                ButtonSavePosition.Dispose ();
                ButtonSavePosition = null;
            }

            if (Map != null) {
                Map.Dispose ();
                Map = null;
            }

            if (TextFieldDescription != null) {
                TextFieldDescription.Dispose ();
                TextFieldDescription = null;
            }

            if (TextFieldName != null) {
                TextFieldName.Dispose ();
                TextFieldName = null;
            }
        }
    }
}