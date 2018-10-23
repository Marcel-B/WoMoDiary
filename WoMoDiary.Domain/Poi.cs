using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.Domain
{
    public class Poi : Place
    {
        public Poi()
        {
            Name = Strings.POINT_OF_INTEREST;
            AssetName = "SightSeeing";
            Type = PlaceType.Poi;
        }
        public override string ToString()
            => Name;
    }
}