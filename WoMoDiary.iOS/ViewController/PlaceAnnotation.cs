using CoreLocation;
using MapKit;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.iOS
{
    public class PlaceAnnotation : MKAnnotation
    {
        Place _place;
        CLLocationCoordinate2D _coord;

        public PlaceAnnotation(Place place,
        CLLocationCoordinate2D coord)
        {
            _place = place;
            _coord = coord;
        }

        public PlaceType PlaceType
             => PlaceType.CampingPlace;

        public override string Title
             => _place.Name;

        public override CLLocationCoordinate2D Coordinate
             => _coord;

    }
}
