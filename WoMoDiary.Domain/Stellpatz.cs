namespace WoMoDiary.Domain
{
    public class Stellpatz : Place
    {
        public Stellpatz()
        {
            Name = "Stellplatz";
            AssetName = "Camping";
            Type = PlaceType.MotorhomePlace;
        }
    }
}