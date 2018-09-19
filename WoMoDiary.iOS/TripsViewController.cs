using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using WoMoDiary.Models;

namespace WoMoDiary.iOS
{
    public partial class TripsViewController : UITableViewController
    {
        IList<Trip> Trips;
        public TripsViewController(IntPtr handle) : base(handle)
        {
            Trips = new List<Trip>();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var store = MockDataStore.GetInstance();
            Trips = (await store.GetItemsAsync()).ToList();
            TableView.ReloadData();
        }
        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return Trips.Count;
        }

        [Export("tableView:heightForRowAtIndexPath:")]
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            => 100f;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MyCell") as TripsTableViewCell;
            cell.Trip = Trips[indexPath.Row].Name;
            cell.DescriptionT = Trips[indexPath.Row].Description;
            return cell;
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TableView.ReloadData();
        }
    }
}