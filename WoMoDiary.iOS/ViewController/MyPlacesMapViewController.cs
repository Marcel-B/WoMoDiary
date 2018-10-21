using System;
using UIKit;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.Domain;
using System.Collections.Generic;
using MapKit;
using CoreLocation;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class MyPlacesMapViewController : UIViewController
    {
        public MyPlacesMapViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ItemMap.Title = Strings.MAP;
            NavigationItem.SetHidesBackButton(true, false);
            SetStates();
        }


        private void SetStates()
        {
            var aa = AppStore.Instance.User.Trips;
            var allPlaces = new List<Place>();
            foreach (var trip in aa)
            {
                allPlaces.AddRange(trip.Places);
            }
            MapViewMyPlaces.RemoveAnnotations(MapViewMyPlaces.Annotations);
            var annotations = new List<MKPointAnnotation>();
            foreach (var place in allPlaces)
            {
                var coordinate = new CLLocationCoordinate2D(place.Latitude, place.Longitude);
                annotations.Add(new MKPointAnnotation
                {
                    Coordinate = coordinate,
                    Title = place.Name
                });
            }
            MapViewMyPlaces.ShowAnnotations(annotations.ToArray(), true);
        }
    }
}