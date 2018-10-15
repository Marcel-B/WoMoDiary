using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;
using WoMoDiary.Helpers;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace WoMoDiary.iOS
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
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
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var store = AppStore.GetInstance();
            store.CurrentPlace = ViewModel.Places[indexPath.Row];
        }
    }
}