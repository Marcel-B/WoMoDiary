using Foundation;
using System;
using UIKit;

namespace WoMoDiary.iOS
{
    public partial class TripsTableViewCell : UITableViewCell
    {
        public string Trip
        {
            get => LableTripName.Text;
            set => LableTripName.Text = value;
        }


        public string DescriptionT
        {
            get => LableTripDescription.Text;
            set => LableTripDescription.Text = value;
        }


        public TripsTableViewCell(IntPtr handle) : base(handle)
        {
        }

    }
}