using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.Domain
{
    public class Hotel : Place
    {
        public Hotel()
        {
            Name = Strings.HOTEL;
            AssetName = "Hotel";
            Type = PlaceType.Hotel;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}