using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.WoMoDiary.Domain;
using Newtonsoft.Json;

namespace com.b_velop.WoMoDiary.Services
{
    public class PlaceDataStore : CloudDataStore<Place>
    {
        public PlaceDataStore()
        {
            Items = new List<Place>();
        }

        protected override string Route => $"api/place/";
        protected override string RouteSpecial => $"api/place/bytrip/";

        public override async Task<IEnumerable<Place>> GetItemsByFkAsync(Guid fk)
        {
            try
            {
                var response = await Client.GetAsync(RouteSpecial + fk);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                var places = JsonConvert.DeserializeObject<IEnumerable<Place>>(content);
                return places;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error while GetItemsByFkAsync in PlaceDataStore with UserId: '{fk}'{Environment.NewLine}Exception Message: '{ex.Message}'");
                return null;
            }
        }
    }
}
