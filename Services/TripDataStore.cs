using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        protected override string RouteSpecial => $"api/trip/byuser/";

        public override async Task<IEnumerable<Trip>> GetItemsByFkAsync(Guid fk)
        {
            try
            {
                var response = await Client.GetAsync(RouteSpecial + fk);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                var trips = JsonConvert.DeserializeObject<IEnumerable<Trip>>(content);
                return trips;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error while GetItemsByFkAsync in TripDataStore with UserId: '{fk}'{Environment.NewLine}Exception Message: '{ex.Message}'");
                return null;
            }
        }
    }
}
