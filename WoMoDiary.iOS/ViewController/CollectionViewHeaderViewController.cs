using Foundation;
using System;
using UIKit;

namespace WoMoDiary.iOS
{
    public partial class CollectionViewHeaderViewController : UICollectionReusableView
    {
        public CollectionViewHeaderViewController(IntPtr handle) : base(handle)
        {

            ButtonAddTrip.TouchUpInside += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Button NewTrip pressed");
            };

        }
    }
}