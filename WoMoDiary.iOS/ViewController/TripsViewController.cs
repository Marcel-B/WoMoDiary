using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using WoMoDiary.Domain;
using WoMoDiary.Services;

namespace WoMoDiary.iOS
{
    public partial class TripsViewController : UITableViewController
    {
        IList<Place> Places;

        public TripsViewController(IntPtr handle) : base(handle)
        {
            Places = new List<Place>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var store = MockDataStore.GetInstance();
            
            var trip = AppStore.GetInstance().CurrentTrip;
            Places = trip.Places;
            TableView.ReloadData();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return Places.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            => 100f;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MyCell") as TripsTableViewCell;
            var trip = Places[indexPath.Row];
            cell.Trip = trip.Name;
            cell.DescriptionT = trip.Description;
            cell.ImagePath.Image = UIImage.FromBundle(trip.AssetName);
            if (!trip.IsGood.HasValue) { }
            else cell.Rating.Image = (bool)trip.IsGood ? UIImage.FromBundle("ThumbUp") : UIImage.FromBundle("ThumbDown");
            //cell.PlacesCount = $"{trip.Places.Count} Places saved.";
            return cell;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TableView.ReloadData();
            var currentTrip = AppStore.GetInstance().CurrentTrip;

            if (currentTrip == null) return;
            //var idx = Places.IndexOf(store.CurrentTrip);

            //if (idx < 0) return;
            //var myIdx = NSIndexPath.FromItemSection(idx, 0);
            //TableView.SelectRow(myIdx, true, UITableViewScrollPosition.Middle);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //var store = AppStore.GetInstance();
            //store.CurrentTrip = Places[indexPath.Row];
        }
    }
}