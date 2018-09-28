using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;
using WoMoDiary.Services;

namespace WoMoDiary.iOS
{
    public partial class PlaceDetailViewController : UIViewController
    {
        public PlaceDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        { 
            base.ViewDidLoad();
            var o = AppStore.GetInstance();
            var i = o.CurrentPlace;
            LabelInfo.Text = i.Description;
            Title = i.Name;
            if (i.Rating > 0)
                ImageRating.Image = UIImage.FromBundle("ThumbUp");
            else
                ImageRating.Image = UIImage.FromBundle("ThumbDown");

            var _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();
            var span = new MKCoordinateSpan(.015, .015);
            var coordinates = new CLLocationCoordinate2D(i.Latitude, i.Longitude);
            Map.Region = new MKCoordinateRegion(coordinates, span);

            Map.RemoveAnnotations();
            Map.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = coordinates,
                Title = i.Name
            });

            //map.RemoveAnnotations();
            //map.AddAnnotation(new MKPointAnnotation
            //{
            //    Coordinate = coordinates,
            //    Title = "Ping!"
            //});
        }
    }
}