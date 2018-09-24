using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using WoMoDiary.Domain;
using System.Net;

namespace WoMoDiary
{
    public class CloudDataStore : IDataStore<TripOtd>
    {
        HttpClient client;
        IEnumerable<TripOtd> items;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<TripOtd>();
        }

        public async Task<IEnumerable<TripOtd>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/trip");
                items = await Task.Run(() => TripOtd.FromJson(json));
            }

            return items;
        }

        public async Task<TripOtd> GetItemAsync(Guid id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<TripOtd>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(TripOtd item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/trip", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(TripOtd item)
        {
            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/trip/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            if (id != Guid.Empty && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/trip/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
