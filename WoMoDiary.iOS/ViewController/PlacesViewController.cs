using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using WoMoDiary.Domain;
using WoMoDiary.Services;

namespace WoMoDiary.iOS
{
    public partial class PlacesViewController : UITableViewController
    {
        IList<Place> Places;
        public int SelectedIndex { get; set; }

        public PlacesViewController(IntPtr handle) : base(handle)
        {
            Places = new List<Place>();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
            => Places.Count;

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            => 100f;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MyCell") as TripsTableViewCell;
            var trip = Places[indexPath.Row];
            cell.Trip = trip.Name;
            cell.DescriptionT = trip.Description;
            cell.ImagePath.Image = UIImage.FromBundle(trip.AssetName);
            cell.Rating.Image = trip.Rating > 0 ? UIImage.FromBundle("ThumbUp") : UIImage.FromBundle("ThumbDown");
            return cell;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var currentTrip = AppStore
                .GetInstance()
                .CurrentTrip;
            if (currentTrip == null) return;
            Places = currentTrip.Places;
            TableView.ReloadData();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var store = AppStore.GetInstance();
            store.CurrentPlace = Places[indexPath.Row];
        }
    }
}