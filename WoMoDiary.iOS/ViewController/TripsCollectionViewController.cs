using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using WoMoDiary.Domain;
using WoMoDiary.Services;

namespace WoMoDiary.iOS
{
    public partial class TripsCollectionViewController : UICollectionViewController
    {
        IList<TripOtd> Trips;
        TripOtd SelectedTrip { get; set; }

        public TripsCollectionViewController(IntPtr handle) : base(handle)
        {
            Trips = new List<TripOtd>();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.SetHidesBackButton(true, false);
            var store = MockDataStore.GetInstance();
            var cloud = new CloudDataStore();

            Trips = (await cloud.GetItemsAsync(true)).ToList();// (await store.GetItemsAsync()).ToList();
            var flowLayout = Layout as UICollectionViewFlowLayout;
            var collectionView = CollectionView;
            var w = collectionView.Frame.Width - 16;
            flowLayout.ItemSize = new CGSize(width: w, height: 120);
            CollectionView.ReloadData();
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
            => Trips.Count();

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = CollectionView.DequeueReusableCell("CollectionViewCell", indexPath) as TripCollectionViewCell;
            var trip = Trips[indexPath.Row];
            cell.Trip = trip.Name;
            cell.Count = $"{trip.Places.Count} places";
            cell.Tag = indexPath.Row;
            cell.TimeSpan = trip.Created.ToString("D");
            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is MainTabBarViewController target)
            {
                if (sender is UICollectionViewCell cell)
                {
                    var trip = Trips[(int)cell.Tag];
                    var store = AppStore.GetInstance();
                    store.CurrentTrip = trip;
                    target.Title = trip.Name;
                }
            }
        }
    }
}