namespace WoMoDiary.Domain
{
    public class CampingPlace : Place
    {
        public CampingPlace()
        {
            Name = "Camping place";
            AssetName = "Camping";
            Type = PlaceType.CampingPlace;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
