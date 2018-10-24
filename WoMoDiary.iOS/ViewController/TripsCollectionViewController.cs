using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class TripsCollectionViewController : UICollectionViewController
    {
        Trip SelectedTrip { get; set; }
        public TripsViewModel ViewModel { get; set; }

        public TripsCollectionViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<TripsViewModel>();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            ItemTrips.Title = Strings.TRIPS;
            var flowLayout = Layout as UICollectionViewFlowLayout;
            var collectionView = CollectionView;
            var width = collectionView.Frame.Width - 16;
            flowLayout.ItemSize = new CGSize(width, 130);
            ViewModel.Trips.CollectionChanged -= Trips_CollectionChanged;
            ViewModel.Trips.CollectionChanged += Trips_CollectionChanged;
            this.ParentViewController.Title = Strings.TRIPS;
            await ViewModel.PullTrips();
        }

        void Trips_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
                App.LogOutLn(ex.Message, GetType().Name);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            CollectionView.ReloadData();
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
            var prefix = trip.Places.Count == 1 ? Strings.PLACE : Strings.PLACES;
            cell.Count = $"{trip.Places.Count} {prefix}";
            cell.Tag = indexPath.Row;
            var span = string.Empty;
            if (trip.Places.Count > 0)
            {
                var last = trip.Places.Max(p => p.Created);
                var first = trip.Places.Min(p => p.Created);
                span = $"{first.ToString("D")} -{Environment.NewLine}{last.ToString("D")}";
            }
            else
                span = trip.Created.ToString("D");

            cell.TimeSpan = span;
            App.LogOutLn($"Cell '{trip.Name}' created.", GetType().Name);
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
                    var store = AppStore.Instance;
                    store.CurrentTrip = trip;
                    target.Title = trip.Name;
                }
            }
        }
    }
}
