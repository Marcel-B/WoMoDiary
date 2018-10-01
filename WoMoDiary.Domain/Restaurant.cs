namespace WoMoDiary.Domain
{
    public class Restaurant : Place
    {
        public Restaurant()
        {
            Name = "Restaurant";
            AssetName = "Restaurant";
            Type = PlaceType.Restaurant;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}