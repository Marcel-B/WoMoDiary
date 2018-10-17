using com.b_velop.WoMoDiary.Meta;
namespace WoMoDiary.Domain
{
    public class Restaurant : Place
    {
        public Restaurant()
        {
            Name = Strings.RESTAURANT;
            AssetName = "Restaurant";
            Type = PlaceType.Restaurant;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}