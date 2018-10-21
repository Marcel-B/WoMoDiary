using Foundation;
using System;
using System.Linq;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class EditPlaceViewController : UIViewController
    {
        public EditPlaceViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ButtonSave.TouchUpInside += (sender, e) =>
            {
                var controllers = NavigationController.ViewControllers;
                NavigationController.SetViewControllers(controllers.SkipLast(1).ToArray(), true);
            };
        }

    }
}