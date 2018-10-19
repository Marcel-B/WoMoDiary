using Foundation;
using System;
using UIKit;
using System.Collections.Specialized;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Services;
using MapKit;
using CoreLocation;
using System.Collections.Generic;
using System.Linq;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class PlacesViewController : UITableViewController
    {
        public int SelectedIndex { get; set; }
        public PlacesViewModel ViewModel { get; set; }
        private List<double> _longitudes = new List<double>();
        private List<double> _latitudes = new List<double>();

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
                    SetMarkerOnMap();
                });
            }
            catch (Exception ex)
            {
                App.LogOutLn(ex.Message);
            }
        }

        private void SetMarkerOnMap()
        {
            MapViewPlaces.RemoveAnnotations();
            var annotations = new List<MKPointAnnotation>();
            foreach (var place in ViewModel.Places)
            {
                var coordinate = new CLLocationCoordinate2D(place.Latitude, place.Longitude);
                annotations.Add(new MKPointAnnotation
                {
                    Coordinate = coordinate,
                    Title = place.Name
                });
            }
            MapViewPlaces.ShowAnnotations(annotations.ToArray(), true);
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
            var place = ViewModel.Places[indexPath.Row];
            cell.Trip = place.Name;
            cell.DescriptionT = place.Description;
            cell.ImagePath.Image = UIImage.FromBundle(place.AssetName);
            cell.Rating.Image = place.Rating > 0 ? UIImage.FromBundle("ThumbUp") : UIImage.FromBundle("ThumbDown");
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
