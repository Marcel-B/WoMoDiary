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
    [Register ("NewTripViewController")]
    partial class NewTripViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSaveTrip { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldTripName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSaveTrip != null) {
                ButtonSaveTrip.Dispose ();
                ButtonSaveTrip = null;
            }

            if (TextFieldDescription != null) {
                TextFieldDescription.Dispose ();
                TextFieldDescription = null;
            }

            if (TextFieldTripName != null) {
                TextFieldTripName.Dispose ();
                TextFieldTripName = null;
            }
        }
    }
}