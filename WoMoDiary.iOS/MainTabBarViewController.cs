using Foundation;
using System;
using UIKit;

namespace WoMoDiary.iOS
{
    public partial class MainTabBarViewController : UITabBarController
    {
        public MainTabBarViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}