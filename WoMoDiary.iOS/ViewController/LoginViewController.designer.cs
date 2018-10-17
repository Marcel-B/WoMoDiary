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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNewUser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonLogin != null) {
                ButtonLogin.Dispose ();
                ButtonLogin = null;
            }

            if (ButtonNewUser != null) {
                ButtonNewUser.Dispose ();
                ButtonNewUser = null;
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