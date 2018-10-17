using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class TripCollectionViewCell : UICollectionViewCell
    {
        public TripCollectionViewCell(IntPtr handle) : base(handle) { }

        public string Trip
        {
            get => LabelTrip.Text;
            set => LabelTrip.Text = value;
        }

        public string Count
        {
            get => LabelCount.Text;
            set => LabelCount.Text = value;
        }

        public string TimeSpan
        {
            get => LabelTimespan.Text;
            set => LabelTimespan.Text = value;
        }
    }
}