using com.b_velop.WoMoDiary.Services;

using CoreLocation;
using MapKit;
using System;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.ViewModels;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class PlaceDetailViewController : UIViewController
    {
        public PlaceDetailViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<PlaceDetailViewModel>();
        }

        public PlaceDetailViewModel ViewModel { get; set; }
        public CLLocationManager LocationManager { get; set; }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetStates();
            //map.RemoveAnnotations();
            //map.AddAnnotation(new MKPointAnnotation
            //{
            //    Coordinate = coordinates,
            //    Title = "Ping!"
            //});
        }

        private void SetStates()
        {
            ViewModel.PullData();
            LabelInfo.Text = ViewModel.Description;
            Title = ViewModel.Name;
            if (ViewModel.Rating > 0)
                ImageRating.Image = UIImage.FromBundle("ThumbUp");
            else
                ImageRating.Image = UIImage.FromBundle("ThumbDown");

            LocationManager = new CLLocationManager();
            LocationManager.RequestWhenInUseAuthorization();

            var span = new MKCoordinateSpan(.015, .015);
            var coordinates = new CLLocationCoordinate2D(ViewModel.Latitude, ViewModel.Longitude);
            Map.Region = new MKCoordinateRegion(coordinates, span);

            Map.RemoveAnnotations();
            Map.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = coordinates,
                Title = ViewModel.Name
            });
        }
    }
}