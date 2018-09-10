using System;

using Foundation;
using UIKit;

namespace WoMoDiary.iOS.CustomViews
{
    public partial class TripCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("TripCollectionViewCell");
        public static readonly UINib Nib;

        public UIImageView TripPicture
        {
            get => ImageTripPicture;
            set => ImageTripPicture = value;
        }

        public string Header
        {
            get => LabelHeader.Text;
            set => LabelHeader.Text = value;
        }

        static TripCollectionViewCell()
        {
            Nib = UINib.FromName("TripCollectionViewCell", NSBundle.MainBundle);
        }

        protected TripCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
