using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class TripDataStore : CloudDataStore<Trip>
    {
        public TripDataStore()
        {
            Items = new List<Trip>();
        }

        protected override string Route => $"api/trip/";
        protected override string RouteSpecial => $"api/place/byuser/";

        public override Task<IEnumerable<Trip>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }
    }
}
