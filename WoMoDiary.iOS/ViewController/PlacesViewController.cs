using Foundation;
using System;
using UIKit;
using System.Collections.Specialized;
using MapKit;
using CoreLocation;
using System.Collections.Generic;

using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.Meta;
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
            MapViewPlaces.RemoveAnnotations(MapViewPlaces.Annotations);
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

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            var alertController = UIAlertController.Create(
                Strings.ATTENTION,
                Strings.DELETE_PLACE(ViewModel.Places[indexPath.Row].Name),
                UIAlertControllerStyle.Alert);

            alertController.AddAction(UIAlertAction.Create(
                Strings.OK,
                UIAlertActionStyle.Default,
                async alert =>
                {
                    App.LogOutLn("Ok clicked", GetType().Name);
                    var result = await ViewModel.DeletePlace(indexPath.Row);
                }));

            alertController.AddAction(UIAlertAction.Create(
                Strings.CANCEL,
                UIAlertActionStyle.Cancel,
                alert => App.LogOutLn("Cancel clicked", GetType().Name)));

            this.PresentViewController(alertController, true, null);
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

        public UIContextualAction ContextualDefinitionAction(int row)
        {
            var place = ViewModel.Places[row];
            var store = AppStore.Instance;
            store.CurrentPlace = place;

            var action = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                Strings.EDIT,
                (ReadLaterAction, view, success) =>
                {
                    PerformSegue("ToEditPlace", this);
                });

            action.BackgroundColor = UIColor.FromRGB(16, 172, 132);
            return action;
        }


        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            //UIContextualActions
            var definitionAction = ContextualDefinitionAction(indexPath.Row);
            //var flagAction = ContextualFlagAction(indexPath.Row);

            //UISwipeActionsConfiguration
            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { /*flagAction,*/ definitionAction });

            leadingSwipe.PerformsFirstActionWithFullSwipe = false;
            return leadingSwipe;
        }
    }
}
