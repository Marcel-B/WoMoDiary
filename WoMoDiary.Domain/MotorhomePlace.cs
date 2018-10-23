using System;
using com.b_velop.WoMoDiary.Meta;

namespace com.b_velop.WoMoDiary.Domain
{
    public class MotorhomePlace : Place
    {
        public MotorhomePlace()
        {
            Name = Strings.PITCH;
            AssetName = "Motorhome";
            Type = PlaceType.MotorhomePlace;
        }
        public override string ToString()
            => Name;
    }
}
