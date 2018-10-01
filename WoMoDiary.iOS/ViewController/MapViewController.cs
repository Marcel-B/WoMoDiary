using System;
using UIKit;
using CoreLocation;
using MapKit;
using WoMoDiary.iOS.Services;
using WoMoDiary.iOS.ViewModels;

namespace WoMoDiary.iOS
{
    public partial class MapViewController : UIViewController
    {
        SaveLocationViewModel viewModel;
        private CLLocationManager _locationManager;

        public MapViewController(IntPtr handle) : base(handle)
        {
            viewModel = new SaveLocationViewModel();
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
                        viewModel.Longitude = coordinates.Longitude;
                        viewModel.Latitude = coordinates.Latitude;
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
                viewModel.Name = ((UITextField)sender).Text;
                ButtonSavePosition.Enabled = viewModel.SaveLocationCommand.CanExecute(null);
            };
            TextFieldDescription.EditingChanged += (sender, e) =>
            {
                viewModel.Description = ((UITextField)sender).Text;
                ButtonSavePosition.Enabled = viewModel.SaveLocationCommand.CanExecute(null);
            };
            ButtonSavePosition.TouchUpInside += (sender, e) =>
            {
                TextFieldName.ResignFirstResponder();
                viewModel.SaveLocationCommand.Execute(null);

            };
        }
    }
}
