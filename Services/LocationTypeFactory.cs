using System;
using WoMoDiary.Domain;
namespace WoMoDiary.Services
{
    public class LocationTypeFactory
    {
        public LocationTypeFactory()
        {
        }

        public static IPlace Create(string type)
        {
            switch (type)
            {
                case "Campingplatz":
                    return new CampingPlace();
                default:
                    return null;
            }
        }
    }
}
