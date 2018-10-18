using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.Domain
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
            => Name;
    }
}