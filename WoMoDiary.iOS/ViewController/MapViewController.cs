using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class MapViewController : UIViewController
    {
        public MapViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<NewPlaceViewModel>();
        }

        public NewPlaceViewModel ViewModel { get; set; }
        public CLLocationManager LocationManager { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Localize();
            SetStates();
            SetControllEvents();

            var gestureRecognizer = new UITapGestureRecognizer(() => View.EndEditing(true));
            gestureRecognizer.CancelsTouchesInView = false; //for iOS5
            View.AddGestureRecognizer(gestureRecognizer);

            LocationManager = new CLLocationManager();
            LocationManager.RequestWhenInUseAuthorization();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is LocationTypeViewController target)
            {
                target.ViewModel = this.ViewModel;
            }
        }

        private void Localize()
        {
            Title = Strings.NEW_PLACE;
            TextFieldName.Placeholder = Strings.ENTER_NAME;
            TextFieldDescription.Placeholder = Strings.ENTER_DESCRIPTION;
            ButtonSavePosition.SetTitle(Strings.NEXT, UIControlState.Normal);
        }

        private void SetStates()
        {
            Map.ShowsUserLocation = true;
        }

        private void SetControllEvents()
        {
            Map.DidUpdateUserLocation += (sender, e) =>
            {

                if (sender is MKMapView map)
                    if (map.UserLocation != null)
                    {
                        var coordinates = map.UserLocation.Coordinate;
                        var span = new MKCoordinateSpan(.015, .015);
                        map.Region = new MKCoordinateRegion(coordinates, span);
                        ViewModel.Longitude = coordinates.Longitude;
                        ViewModel.Latitude = coordinates.Latitude;
                    }
            };
            TextFieldName.ShouldReturn = textField =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            TextFieldDescription.ShouldReturn = textField =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            TextFieldName.EditingChanged += (sender, e) =>
            {
                ViewModel.Name = ((UITextField)sender).Text;
                ButtonSavePosition.Enabled = ViewModel.SavePlaceCommand.CanExecute(null);
            };
            TextFieldDescription.EditingChanged += (sender, e) =>
            {
                ViewModel.Description = ((UITextField)sender).Text;
                ButtonSavePosition.Enabled = ViewModel.SavePlaceCommand.CanExecute(null);
            };
            ButtonSavePosition.TouchUpInside += (sender, e) =>
            {
                TextFieldName.ResignFirstResponder();
            };
        }
    }
}
