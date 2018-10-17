using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using WoMoDiary;
using com.b_velop.WoMoDiary.Meta;

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
            var flowLayout = Layout as UICollectionViewFlowLayout;
            var collectionView = CollectionView;
            var width = collectionView.Frame.Width - 16;
            flowLayout.ItemSize = new CGSize(width, 120);
            ViewModel.Trips.CollectionChanged -= Trips_CollectionChanged;
            ViewModel.Trips.CollectionChanged += Trips_CollectionChanged;
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Title = Strings.TRIPS;
            NavigationItem.SetHidesBackButton(true, false);
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
            cell.TimeSpan = trip.Created.ToString("D");
            App.LogOutLn($"Cell {trip.Name} created.");
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
