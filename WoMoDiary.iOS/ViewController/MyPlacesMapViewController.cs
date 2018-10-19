using Foundation;
using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class MyPlacesMapViewController : UIViewController
    {
        public MyPlacesMapViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}