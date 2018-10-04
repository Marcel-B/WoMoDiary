using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class PlaceDataStore : CloudDataStore<Place>
    {
        public PlaceDataStore()
        {
            Items = new List<Place>();
        }

        protected override string Route => $"api/place/";
        protected override string RouteSpecial => $"api/place/bytrip/";

        public override Task<IEnumerable<Place>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }
    }
}
