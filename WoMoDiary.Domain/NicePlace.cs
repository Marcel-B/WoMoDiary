namespace WoMoDiary.Domain
{
    public class NicePlace : Place
    {
        public NicePlace()
        {
            Name = "Nice place";
            AssetName = "SightSeeing";
            Type = PlaceType.SightSeeing;
        }
        public override string ToString()
            => Name;
    }
}