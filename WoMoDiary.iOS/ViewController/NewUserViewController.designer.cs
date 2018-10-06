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
    [Register ("NewUserViewController")]
    partial class NewUserViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonConfirm { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldConfirmPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonConfirm != null) {
                ButtonConfirm.Dispose ();
                ButtonConfirm = null;
            }

            if (TextFieldConfirmPassword != null) {
                TextFieldConfirmPassword.Dispose ();
                TextFieldConfirmPassword = null;
            }

            if (TextFieldEmail != null) {
                TextFieldEmail.Dispose ();
                TextFieldEmail = null;
            }

            if (TextFieldPassword != null) {
                TextFieldPassword.Dispose ();
                TextFieldPassword = null;
            }

            if (TextFieldUsername != null) {
                TextFieldUsername.Dispose ();
                TextFieldUsername = null;
            }
        }
    }
}