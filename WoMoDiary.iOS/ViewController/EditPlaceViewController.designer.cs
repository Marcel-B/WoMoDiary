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
    [Register ("EditPlaceViewController")]
    partial class EditPlaceViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint BottomSpaceButtonSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSave { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BottomSpaceButtonSave != null) {
                BottomSpaceButtonSave.Dispose ();
                BottomSpaceButtonSave = null;
            }

            if (ButtonSave != null) {
                ButtonSave.Dispose ();
                ButtonSave = null;
            }
        }
    }
}