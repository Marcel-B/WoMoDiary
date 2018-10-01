using System.Collections.Generic;
using WoMoDiary.Domain;

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
