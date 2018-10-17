using com.b_velop.WoMoDiary.Meta;
namespace WoMoDiary.Domain
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
        {
            return Name;
        }
    }
}
