using System;
using WoMoDiary.Domain;
using System.Collections.Generic;

namespace WoMoDiary.Services
{
    public class PlaceDataStore : CloudDataStore<Place>
    {
        protected override string Route
        {
            get => $"api/place/";
            set => Route = value;
        }

        protected override string RouteSpecial
        {
            get => $"api/place/bytrip/";
            set => Route = value;
        }

        public PlaceDataStore()
        {
            items = new List<Place>();
        }
    }
}
