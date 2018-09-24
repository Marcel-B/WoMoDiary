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
    public class CloudDataStore : IDataStore<Trip>
    {
        HttpClient client;
        IEnumerable<Trip> items;

        public CloudDataStore()
        {
            client = new HttpClient();
            //client.BaseAddress = new Uri($"{App.BackendUrl}/");
            client.BaseAddress = new Uri($"http://localhost:5000/");

            items = new List<Trip>();
        }

        public async Task<IEnumerable<Trip>> GetItemsAsync(bool forceRefresh = false)
        {
            //var aclient = new HttpClient();
            //var foo = await aclient.GetAsync("https://womo.marcelbenders.de/api/trip");
            //var rest = await foo.Content.ReadAsStringAsync();
            //if (forceRefresh && CrossConnectivity.Current.IsConnected)
            //{
                var json = await client.GetStringAsync($"api/trip");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Trip>>(json));
            //}

            return items;
        }

        public async Task<Trip> GetItemAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Trip>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Trip item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/trip", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Trip item)
        {
            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/trip/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/trip/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
