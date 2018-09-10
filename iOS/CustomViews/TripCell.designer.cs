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
    [Register ("TripCell")]
    partial class TripCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Destination { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Second { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Destination != null) {
                Destination.Dispose ();
                Destination = null;
            }

            if (Second != null) {
                Second.Dispose ();
                Second = null;
            }
        }
    }
}