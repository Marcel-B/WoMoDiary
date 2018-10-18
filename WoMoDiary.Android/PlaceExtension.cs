using System;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.Android
{
    public static class PlaceExtension
    {
        public static int ToRating(this Place place)
           => place.Rating > 2 ? Resource.Drawable.thumb_up_light : Resource.Drawable.thumb_down_light;

        public static int ToCategory(this Place place)
        {
            switch (place.Type)
            {
                case PlaceType.Hotel:
                    return Resource.Drawable.hotel_light;
                case PlaceType.CampingPlace:
                    return Resource.Drawable.camping_light;
                case PlaceType.MotorhomePlace:
                    return Resource.Drawable.camping_light;
                case PlaceType.Restaurant:
                    return Resource.Drawable.restaurant_light;
                case PlaceType.SightSeeing:
                    return Resource.Drawable.sightseeing_light;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
