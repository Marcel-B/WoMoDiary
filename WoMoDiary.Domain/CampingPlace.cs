namespace WoMoDiary.Domain
{
    public class CampingPlace : Place
    {
        public CampingPlace()
        {
            Name = "Camping place";
            AssetName = "Camping";
            Type = PlaceType.CampingPlace;
        }
    }

    public class Stellpatz : Place
    {
        public Stellpatz()
        {
            Name = "Stellplatz";
            AssetName = "Camping";
            Type = PlaceType.MotorhomePlace;
        }
    }

    public class Restaurant : Place
    {
        public Restaurant()
        {
            Name = "Restaurant";
            AssetName = "Restaurant";
            Type = PlaceType.Restaurant;
        }
    }

    public class NicePlace : Place
    {
        public NicePlace()
        {
            Name = "Nice place";
            AssetName = "SightSeeing";
            Type = PlaceType.SightSeeing;
        }
    }

    public class Hotel : Place
    {
        public Hotel()
        {
            Name = "Hotel";
            AssetName = "Hotel";
            Type = PlaceType.Hotel;
        }
    }
}
