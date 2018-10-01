using System;
using UIKit;
using CoreLocation;
using MapKit;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;
using Foundation;

namespace WoMoDiary.iOS
{
    public partial class MapViewController : UIViewController
    {
        private NewPlaceViewModel _viewModel;
        private CLLocationManager _locationManager;

        public MapViewController(IntPtr handle) : base(handle)
        {
            _viewModel = new NewPlaceViewModel();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var store = AppStore.GetInstance();
            if (store.CurrentTrip == null) return;
            this.Title = $"- {store.CurrentTrip.Name} -";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();
            Map.ShowsUserLocation = true;

            Map.DidUpdateUserLocation += (sender, e) =>
            {

                if (sender is MKMapView map)
                    if (map.UserLocation != null)
                    {
                        var coordinates = map.UserLocation.Coordinate;
                        var span = new MKCoordinateSpan(.015, .015);
                        map.Region = new MKCoordinateRegion(coordinates, span);
                        _viewModel.Longitude = coordinates.Longitude;
                        _viewModel.Latitude = coordinates.Latitude;
                    }
            };

            TextFieldName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            TextFieldDescription.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            TextFieldName.EditingChanged += (sender, e) =>
            {
                _viewModel.Name = ((UITextField)sender).Text;
                ButtonSavePosition.Enabled = _viewModel.SavePlaceCommand.CanExecute(null);
            };
            TextFieldDescription.EditingChanged += (sender, e) =>
            {
                _viewModel.Description = ((UITextField)sender).Text;
                ButtonSavePosition.Enabled = _viewModel.SavePlaceCommand.CanExecute(null);
            };
            ButtonSavePosition.TouchUpInside += (sender, e) =>
            {
                TextFieldName.ResignFirstResponder();
                _viewModel.SavePlaceCommand.Execute(null);
            };
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is LocationTypeViewController target)
            {
                target.ViewModel = this._viewModel;
            }
        }
    }
}
