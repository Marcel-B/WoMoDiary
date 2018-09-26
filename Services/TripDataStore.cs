using System.Collections.Generic;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class TripDataStore : CloudDataStore<Trip>
    {
        protected override string Route
        {
            get => $"api/trip/";
            set => Route = value;
        }

        public TripDataStore()
        {
            items = new List<Trip>();
        }
    }
}
