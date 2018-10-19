using Foundation;
using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class TabBarViewController : UITabBarController
    {
        public TabBarViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}