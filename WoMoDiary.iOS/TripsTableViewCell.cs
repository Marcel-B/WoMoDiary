using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class TripsTableViewCell : UITableViewCell
    {
        public TripsTableViewCell(IntPtr handle) : base(handle) { }

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

        public UIImageView Rating
        {
            get => ImageRating;
            set => ImageRating = value;
        }

        public UIImageView ImagePath
        {
            get => ImageType;
            set => ImageType = value;
        }
    }
}