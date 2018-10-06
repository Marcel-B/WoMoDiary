using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;
using System.Collections.ObjectModel;
using I18NPortable;
using WoMoDiary.Helpers;

namespace WoMoDiary.iOS
{
    public partial class TripsCollectionViewController : UICollectionViewController
    {
        Trip SelectedTrip { get; set; }
        public TripsViewModel ViewModel { get; set; }

        public TripsCollectionViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var flowLayout = Layout as UICollectionViewFlowLayout;
            var collectionView = CollectionView;
            var w = collectionView.Frame.Width - 16;
            flowLayout.ItemSize = new CGSize(width: w, height: 120);
            if (ViewModel == null)
            {
                ViewModel = ServiceLocator.Instance.Get<TripsViewModel>();
                ViewModel.Trips.CollectionChanged += (sender, e) =>
                {
                    Reload();
                };
            }
        }

        public void Reload()
        {
            try
            {
                BeginInvokeOnMainThread(() =>
                {
                    CollectionView.ReloadData();
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Title =  "Trips".Translate();
            NavigationItem.SetHidesBackButton(true, false);
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
                => ViewModel.Trips.Count();

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = CollectionView.DequeueReusableCell("CollectionViewCell", indexPath) as TripCollectionViewCell;
            var trip = ViewModel.Trips[indexPath.Row];
            cell.Trip = trip.Name;
            if (trip.Places == null)
                trip.Places = new List<Place>();
            cell.Count = $"{trip.Places.Count} places";
            cell.Tag = indexPath.Row;
            cell.TimeSpan = trip.Created.ToString("D");
            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            var s = segue;
            var i = segue.DestinationViewController;
            if (segue.DestinationViewController is PlacesViewController target)
            {
                if (sender is UICollectionViewCell cell)
                {
                    var trip = ViewModel.Trips[(int)cell.Tag];
                    var store = AppStore.GetInstance();
                    store.CurrentTrip = trip;
                    target.Title = trip.Name;
                }
            }
        }
    }
}
