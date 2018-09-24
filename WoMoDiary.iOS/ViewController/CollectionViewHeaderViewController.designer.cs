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
    [Register ("CollectionViewHeaderViewController")]
    partial class CollectionViewHeaderViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonAddTrip { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonAddTrip != null) {
                ButtonAddTrip.Dispose ();
                ButtonAddTrip = null;
            }
        }
    }
}