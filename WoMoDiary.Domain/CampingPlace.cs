using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.Domain
{
    public class CampingPlace : Place
    {
        public CampingPlace()
        {
            Name = Strings.CAMPING_SITE;
            AssetName = "Camping";
            Type = PlaceType.CampingPlace;
        }

        public override string ToString()
            => Name;
    }
}
