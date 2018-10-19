using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{

    public partial class NewPlaceMapViewController : UIViewController
    {
        MapDelegate mapDelegate;

        public NewPlaceMapViewController(IntPtr handle) : base(handle)
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
            UITapGestureRecognizer gestureRecognizer = null;

            gestureRecognizer = new UITapGestureRecognizer(() =>
            {
                View.EndEditing(true);
                Map.RemoveAnnotations(Map.Annotations);
                CGPoint touchPoint = gestureRecognizer.LocationInView(Map);
                var loc = Map.ConvertPoint(touchPoint, Map);
                ViewModel.Latitude = loc.Latitude;
                ViewModel.Longitude = loc.Longitude;
                var coordinates = new CLLocationCoordinate2D(ViewModel.Latitude, ViewModel.Longitude);
                var ann = new MKPointAnnotation
                {
                    Coordinate = coordinates,
                    Title = Strings.YOU_ARE_HERE
                };
                Map.AddAnnotation(ann);
                var span = new MKCoordinateSpan(.015, .015);
                Map.Region = new MKCoordinateRegion(coordinates, span);
                App.LogOutLn($"Longitude {loc.Longitude} | Latitude {loc.Latitude}", GetType().Name);
            });

            View.AddGestureRecognizer(gestureRecognizer);
            LocationManager = new CLLocationManager();
            LocationManager.RequestWhenInUseAuthorization();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is NewPlaceLocationTypeViewController target)
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
            //Map.DidSelectAnnotationView += (sender, e) =>
            //{
            //    App.LogOutLn("TestTest", GetType().Name);
            //};
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
                        LocationManager.StopUpdatingLocation();
                        coordinates = new CLLocationCoordinate2D(ViewModel.Latitude, ViewModel.Longitude);
                        Map.Region = new MKCoordinateRegion(coordinates, span);

                        Map.RemoveAnnotations();
                        Map.AddAnnotation(new MKPointAnnotation
                        {
                            Coordinate = coordinates,
                            Title = Strings.YOU_ARE_HERE
                        });
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
