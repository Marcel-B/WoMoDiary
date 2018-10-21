using System;
using UIKit;
using com.b_velop.WoMoDiary.Meta;

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
            ItemMap.Title = Strings.MAP;
            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}