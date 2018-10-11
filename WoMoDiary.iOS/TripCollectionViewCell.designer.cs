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
    [Register ("TripCollectionViewCell")]
    partial class TripCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelTimespan { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelTrip { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LabelCount != null) {
                LabelCount.Dispose ();
                LabelCount = null;
            }

            if (LabelTimespan != null) {
                LabelTimespan.Dispose ();
                LabelTimespan = null;
            }

            if (LabelTrip != null) {
                LabelTrip.Dispose ();
                LabelTrip = null;
            }
        }
    }
}