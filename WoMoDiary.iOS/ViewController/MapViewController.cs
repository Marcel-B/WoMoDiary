using System;
using UIKit;
using CoreLocation;
using MapKit;
using WoMoDiary.Services;
using WoMoDiary.ViewModels;
using Foundation;
using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class MapViewController : UIViewController
    {
        public NewPlaceViewModel ViewModel { get; set; }
        public CLLocationManager LocationManager { get; set; }

        public MapViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new NewPlaceViewModel();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var store = AppStore.Instance;
            if (store.CurrentTrip == null) return;
            this.Title = $"{store.CurrentTrip.Name}";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Localize();
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            LocationManager = new CLLocationManager();
            LocationManager.RequestWhenInUseAuthorization();
            Map.ShowsUserLocation = true;

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

        private void Localize()
        {
            TextFieldName.Placeholder = Strings.ENTER_NAME;
            TextFieldDescription.Placeholder = Strings.ENTER_DESCRIPTION;
            ButtonSavePosition.SetTitle(Strings.NEXT, UIControlState.Normal);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is LocationTypeViewController target)
            {
                target.ViewModel = this.ViewModel;
            }
        }
    }
}
