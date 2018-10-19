using CoreGraphics;
using CoreLocation;
using MapKit;
using UIKit;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.iOS
{
    public class MapDelegate : MKMapViewDelegate
    {
        static string annotationId = "ConferenceAnnotation";
        UIImageView venueView;
        UIImage venueImage;

        public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
        {
            var coordinates = mapView.UserLocation.Coordinate;
            var span = new MKCoordinateSpan(.015, .015);
            mapView.Region = new MKCoordinateRegion(coordinates, span);
            mapView.AddAnnotation(new PlaceAnnotation(new CampingPlace(), coordinates));
        }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            if (annotation is PlaceAnnotation)
            {

                // show conference annotation
                annotationView = mapView.DequeueReusableAnnotation(annotationId);

                if (annotationView == null)
                    annotationView = new MKAnnotationView(annotation, annotationId);

                //annotationView.Image = UIImage.FromFile("images/conference.png");
                annotationView.CanShowCallout = true;
            }

            return annotationView;
        }
        public override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            // show an image view when the conference annotation view is selected
            if (view.Annotation is PlaceAnnotation)
            {

                venueView = new UIImageView();
                venueView.ContentMode = UIViewContentMode.ScaleAspectFit;
                //venueImage = UIImage.FromFile("image/venue.png");
                venueView.Image = venueImage;
                view.AddSubview(venueView);

                UIView.Animate(0.4, () =>
                {
                    venueView.Frame = new CGRect(-75, -75, 200, 200);
                });
            }
        }
        public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
        {
            // return a view for the polygon
            MKPolygon polygon = overlay as MKPolygon;
            MKPolygonView polygonView = new MKPolygonView(polygon);
            polygonView.FillColor = UIColor.Blue;
            polygonView.StrokeColor = UIColor.Red;
            return polygonView;
        }
        public override void DidDeselectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            // remove the image view when the conference annotation is deselected
            if (view.Annotation is PlaceAnnotation)
            {
                venueView.RemoveFromSuperview();
                venueView.Dispose();
                venueView = null;
            }
        }

    }
}
