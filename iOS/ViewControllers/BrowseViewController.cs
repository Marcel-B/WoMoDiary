using System;
using System.Collections.Specialized;
using CoreLocation;
using Foundation;
using UIKit;
using WoMoDiary.iOS.CustomViews;
using WoMoDiary.iOS.HelpersIOS;

namespace WoMoDiary.iOS
{
    public partial class BrowseViewController : UITableViewController
    {
        UIRefreshControl refreshControl;

        public ItemsViewModel ViewModel { get; set; }

        public BrowseViewController(IntPtr handle) : base(handle)
        {
            //Manager = new LocationManager();
            //Manager.StartLocationUpdates();
        }

        public static LocationManager Manager { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();



            // It is better to handle this with notifications, so that the UI updates
            // resume when the application re-enters the foreground!
            //Manager.LocationUpdated += HandleLocationChanged;

            // Screen subscribes to the location changed event
            //UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
            //{
            //    Manager.LocationUpdated += HandleLocationChanged;
            //});

            // Whenever the app enters the background state, we unsubscribe from the event 
            // so we no longer perform foreground updates
            //UIApplication.Notifications.ObserveDidEnterBackground((sender, args) =>
            //{
            //    Manager.LocationUpdated -= HandleLocationChanged;
            //});


            TableView.RegisterNibForCellReuse(UINib.FromName("TripCell", NSBundle.MainBundle), "TripCell");
            TableView.RowHeight = 125;

            ViewModel = new ItemsViewModel();

            // Setup UITableView.
            refreshControl = new UIRefreshControl();
            refreshControl.ValueChanged += RefreshControl_ValueChanged;
            TableView.Add(refreshControl);
            TableView.Source = new ItemsDataSource(ViewModel);

            Title = ViewModel.Title;

            ViewModel.PropertyChanged += IsBusy_PropertyChanged;
            ViewModel.Items.CollectionChanged += Items_CollectionChanged;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (ViewModel.Items.Count == 0)
                ViewModel.LoadItemsCommand.Execute(null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToItemDetailSegue")
            {
                var controller = segue.DestinationViewController as BrowseItemDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                var item = ViewModel.Items[indexPath.Row];

                controller.ViewModel = new ItemDetailViewModel(item);
            }
            else
            {
                var controller = segue.DestinationViewController as NewTripViewController;
                controller.ViewModel = ViewModel;
            }
        }

        void RefreshControl_ValueChanged(object sender, EventArgs e)
        {
            if (!ViewModel.IsBusy && refreshControl.Refreshing)
                ViewModel.LoadItemsCommand.Execute(null);
        }

        void IsBusy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName;
            switch (propertyName)
            {
                case nameof(ViewModel.IsBusy):
                    {
                        InvokeOnMainThread(() =>
                        {
                            if (ViewModel.IsBusy && !refreshControl.Refreshing)
                                refreshControl.BeginRefreshing();
                            else if (!ViewModel.IsBusy)
                                refreshControl.EndRefreshing();
                        });
                    }
                    break;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //tableView.
            base.RowSelected(tableView, indexPath);
        }

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvokeOnMainThread(() => TableView.ReloadData());
        }


        public void HandleLocationChanged(object sender, LocationUpdatedEventArgs e)
        {
            // Handle foreground updates
            CLLocation location = e.Location;

            //Title = location.Altitude + " meters";
            //lblLongitude.Text = location.Coordinate.Longitude.ToString();
            //lblLAtitude.Text = location.Coordinate.Latitude.ToString();
            //lblCourse.Text = location.Course.ToString();
            Title = location.Speed.ToString();

            Console.WriteLine("foreground updated");
        }
    }

    class ItemsDataSource : UITableViewSource
    {
        //static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL");
        static readonly NSString CELL_IDENTIFIER = new NSString("TripCell");

        ItemsViewModel viewModel;

        public ItemsDataSource(ItemsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => viewModel.Items.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath) as TripCell;

            var item = viewModel.Items[indexPath.Row];
            cell.DestinationLabel = item.Text;
            cell.SecondLabel = DateTime.Now.ToString();// item.Description;
            cell.LayoutMargins = UIEdgeInsets.Zero;

            return cell;
        }
    }
}
