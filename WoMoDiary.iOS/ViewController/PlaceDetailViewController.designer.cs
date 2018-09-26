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
    [Register ("PlaceDetailViewController")]
    partial class PlaceDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageRating { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelInfo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView Map { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImageRating != null) {
                ImageRating.Dispose ();
                ImageRating = null;
            }

            if (LabelInfo != null) {
                LabelInfo.Dispose ();
                LabelInfo = null;
            }

            if (Map != null) {
                Map.Dispose ();
                Map = null;
            }
        }
    }
}