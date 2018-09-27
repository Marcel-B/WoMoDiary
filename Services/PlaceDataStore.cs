using WoMoDiary.Domain;
using System.Collections.Generic;

namespace WoMoDiary.Services
{
    public class PlaceDataStore : CloudDataStore<Place>
    {
        protected override string Route
        {
            get => $"api/place/";
        }

        protected override string RouteSpecial
        {
            get => $"api/place/bytrip/";
        }

        public PlaceDataStore()
        {
            items = new List<Place>();
        }
    }
}
