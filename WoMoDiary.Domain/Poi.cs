using WoMoDiary.Meta;

namespace WoMoDiary.Domain
{
    public class Poi : Place
    {
        public Poi()
        {
            Name = Strings.POINT_OF_INTEREST;
            AssetName = "SightSeeing";
            Type = PlaceType.SightSeeing;
        }
        public override string ToString()
            => Name;
    }
}