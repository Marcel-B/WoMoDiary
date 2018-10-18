using Foundation;
using System;
using UIKit;
using System.Collections.Specialized;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class PlacesViewController : UITableViewController
    {
        public int SelectedIndex { get; set; }
        public PlacesViewModel ViewModel { get; set; }

        public PlacesViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<PlacesViewModel>();
        }

        public void Reload(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                BeginInvokeOnMainThread(() =>
                {
                    TableView.ReloadData();
                });
            }
            catch (Exception ex)
            {
                App.LogOutLn(ex.Message);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewModel.Places.CollectionChanged -= Reload;
            ViewModel.Places.CollectionChanged += Reload;
            ViewModel.PullPlaces();
        }
        public override nint RowsInSection(UITableView tableView, nint section)
            => ViewModel.Places.Count;

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            => 100f;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MyCell") as TripsTableViewCell;
            var trip = ViewModel.Places[indexPath.Row];
            cell.Trip = trip.Name;
            cell.DescriptionT = trip.Description;
            cell.ImagePath.Image = UIImage.FromBundle(trip.AssetName);
            cell.Rating.Image = trip.Rating > 0 ? UIImage.FromBundle("ThumbUp") : UIImage.FromBundle("ThumbDown");
            return cell;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.Places.CollectionChanged -= Reload;
            ViewModel.Places.Clear();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var store = AppStore.Instance;
            store.CurrentPlace = ViewModel.Places[indexPath.Row];
        }
    }
}
