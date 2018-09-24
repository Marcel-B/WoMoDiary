using System;
using UIKit;

namespace WoMoDiary.iOS
{
    public partial class TripCollectionViewCell : UICollectionViewCell
    {
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
        public TripCollectionViewCell(IntPtr handle) : base(handle)
        {
            //ContentView.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);
        }

        //public override UICollectionViewLayoutAttributes PreferredLayoutAttributesFittingAttributes(UICollectionViewLayoutAttributes layoutAttributes)
        //{
        //    SetNeedsLayout();
        //    LayoutIfNeeded();
        //    var size = ContentView. SystemLayoutSizeFittingSize(layoutAttributes.Size);
        //    var s = UIScreen.MainScreen.Bounds.Size;
        //    layoutAttributes.Size = new CGSize(88, 88);
        //    // note: don't change the width
        //    return layoutAttributes;
        //}

    }
}