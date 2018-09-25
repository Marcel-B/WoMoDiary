using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using WoMoDiary.Domain;
using System.Linq;

namespace WoMoDiary
{
    public class PlaceDataStore : CloudDataStore<Place>
    {
        protected override string Route
        {
            get => $"api/place/";
            set => Route = value;
        }
    }

    public abstract class CloudDataStore<T> : IDataStore<T> where T : IItem
    {
        protected HttpClient client;
        protected IEnumerable<T> items;
        protected abstract string Route { get; set; }
        protected CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");
        }

        public async Task<bool> AddItemAsync(T item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;
            var serializedItem = JsonConvert.SerializeObject(item);
            var response = await client.PostAsync(Route, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            if (id != Guid.Empty && !CrossConnectivity.Current.IsConnected)
                return false;
            var response = await client.DeleteAsync(Route + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync(Route + id);
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }
            return default(T);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync(Route);
                var item = JsonConvert.DeserializeObject<T>(json);
                items.Append(item);
            }
            return items;
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;
            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);
            var response = await client.PutAsync(new Uri(Route + item.Id), byteContent);
            return response.IsSuccessStatusCode;
        }
    }

    public class TripDataStore : CloudDataStore<TripOtd>
    {
        protected override string Route
        {
            get => $"api/trip/";
            set => Route = value;
        }

        public TripDataStore()
        {
            items = new List<TripOtd>();
        }
    }
}
