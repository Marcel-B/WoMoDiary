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
    [Register ("TripsTableViewCell")]
    partial class TripsTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageRating { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageType { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LableTripDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LableTripName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImageRating != null) {
                ImageRating.Dispose ();
                ImageRating = null;
            }

            if (ImageType != null) {
                ImageType.Dispose ();
                ImageType = null;
            }

            if (LableTripDescription != null) {
                LableTripDescription.Dispose ();
                LableTripDescription = null;
            }

            if (LableTripName != null) {
                LableTripName.Dispose ();
                LableTripName = null;
            }
        }
    }
}