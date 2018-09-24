using System;
using Foundation;
using UIKit;

namespace WoMoDiary.iOS
{
    public partial class MainTabBarViewController : UITabBarController
    {
        public MainTabBarViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //NavigationItem.BackBarButtonItem.Title = "Trips";
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {

        }
    }
}