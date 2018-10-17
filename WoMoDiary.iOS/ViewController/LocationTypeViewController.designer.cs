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
    [Register ("LocationTypeViewController")]
    partial class LocationTypeViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonThumbDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonThumbUp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelSelectPlaceCategory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerViewLocationType { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSave != null) {
                ButtonSave.Dispose ();
                ButtonSave = null;
            }

            if (ButtonThumbDown != null) {
                ButtonThumbDown.Dispose ();
                ButtonThumbDown = null;
            }

            if (ButtonThumbUp != null) {
                ButtonThumbUp.Dispose ();
                ButtonThumbUp = null;
            }

            if (LabelSelectPlaceCategory != null) {
                LabelSelectPlaceCategory.Dispose ();
                LabelSelectPlaceCategory = null;
            }

            if (PickerViewLocationType != null) {
                PickerViewLocationType.Dispose ();
                PickerViewLocationType = null;
            }
        }
    }
}