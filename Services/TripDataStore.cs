using System.Collections.Generic;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class TripDataStore : CloudDataStore<Trip>
    {
        protected override string Route
        {
            get => $"api/trip/";
        }
        protected override string RouteSpecial
        {
            get => $"api/place/byuser/";
        }
        public TripDataStore()
        {
            items = new List<Trip>();
        }
    }
}
