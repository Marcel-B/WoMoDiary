using CoreGraphics;
using Foundation;
using System;
using UIKit;
using WoMoDiary.iOS.CustomViews;

namespace WoMoDiary.iOS
{
    public partial class CollectionViewController : UICollectionViewController
    {
        public UICollectionViewDelegate CollectionViewDelegate { get; set; }

        public string[] foos = new[]
        { "Eins", "Zwei", "Drei", "Vier", "Fünf", "Sechs",
            "Sieben", "Acht", "Neun", "Zehn", "Elf", "Zwölf", "Dreizehn" };
        public CollectionViewController(IntPtr handle) : base(handle)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //var layout = new UICollectionViewFlowLayout();
            var layout = new UICollectionViewFlowLayout
            {
                ItemSize = new CGSize((float)220, (float)280),
                //ItemSize = new CGSize((float)UIScreen.MainScreen.Bounds.Size.Width / 2.0f, (float)UIScreen.MainScreen.Bounds.Size.Width / 2.0f),
                HeaderReferenceSize = new CGSize(10, 0),
                SectionInset = new UIEdgeInsets(0, 0, 0, 0),
                ScrollDirection = UICollectionViewScrollDirection.Horizontal,
                MinimumInteritemSpacing = 2, // minimum spacing between cells
                MinimumLineSpacing = 14 // minimum spacing between rows if ScrollDirection is Vertical or between columns if Horizontal
            };
            CollectionView.CollectionViewLayout.InvalidateLayout();
            CollectionView.SetCollectionViewLayout(layout, false);
            CollectionView.RegisterNibForCell(UINib.FromName("TripCollectionViewCell", NSBundle.MainBundle), "TripCollectionViewCell");
            //TableView.RowHeight = 125;
        }


        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("TripCollectionViewCell", indexPath) as TripCollectionViewCell;
            cell.Header = foos[indexPath.Row];

            cell.BackgroundView = new UIView { BackgroundColor = UIColor.FromRGB(253, 150, 68) };
            cell.SelectedBackgroundView = new UIView { BackgroundColor = UIColor.Green };

            //cell.ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
            //cell.ContentView.Layer.BorderWidth = 10.0f;
            //cell.ContentView.BackgroundColor = UIColor.T;
            //cell.ContentView.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);
            cell.Header = foos[indexPath.Row];

            //cell.ContentView.BackgroundColor = UIColor.Yellow;

            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return foos.Length;
        }
    }
}