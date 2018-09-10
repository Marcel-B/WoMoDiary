using System;

using Foundation;
using UIKit;

namespace WoMoDiary.iOS.CustomViews
{
    public partial class TripCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TripCell");
        public static readonly UINib Nib;

        public string DestinationLabel { get => Destination.Text; set => Destination.Text = value; }
        public string SecondLabel { get => Second.Text; set => Second.Text = value; }


        static TripCell()
        {
            Nib = UINib.FromName("TripCell", NSBundle.MainBundle);
        }

        protected TripCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
