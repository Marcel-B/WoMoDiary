using com.b_velop.WoMoDiary.Meta;
namespace WoMoDiary.Domain
{
    public class Stellpatz : Place
    {
        public Stellpatz()
        {
            Name = Strings.PITCH;
            AssetName = "Camping";
            Type = PlaceType.MotorhomePlace;
        }

        public override string ToString()
            => Name;
    }
}