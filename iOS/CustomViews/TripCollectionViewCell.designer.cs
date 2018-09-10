// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace WoMoDiary.iOS.CustomViews
{
    [Register ("TripCollectionViewCell")]
    partial class TripCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageTripPicture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelHeader { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImageTripPicture != null) {
                ImageTripPicture.Dispose ();
                ImageTripPicture = null;
            }

            if (LabelHeader != null) {
                LabelHeader.Dispose ();
                LabelHeader = null;
            }
        }
    }
}