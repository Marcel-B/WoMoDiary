namespace WoMoDiary.Domain
{
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