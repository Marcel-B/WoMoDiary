using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public abstract class CloudDataStore<T> : IDataStore<T> where T : IItem
    {
        protected HttpClient client;
        protected IEnumerable<T> items;
        protected abstract string Route { get; }
        protected abstract string RouteSpecial { get; }
        protected CloudDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BackendUrl}/")
            };
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
                var clientOne = new HttpClient();

                var jj = await clientOne.GetAsync($"{App.BackendUrl}/{Route + id.ToString()}");
                if (jj.IsSuccessStatusCode) {
                    var j = await jj.Content.ReadAsStringAsync();
                    return await Task.Run(() => JsonConvert.DeserializeObject<T>(j));
                }
                //var json = await client.GetStringAsync(Route + id);
            }
            return default(T);
        }
        public async Task<IEnumerable<T>> GetItemsAsync(Guid id, bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var result = await client.GetAsync(RouteSpecial + id.ToString());
                if (!result.IsSuccessStatusCode)
                    return new List<T>();
                var json = await result.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<T[]>(json);
            }
            return items;
        }
        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync(Route);
                items = JsonConvert.DeserializeObject<T[]>(json);
            }
            return items;
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;
            var serializedItem = JsonConvert.SerializeObject(item);
            var clientOne = new HttpClient();
            var cnt = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);
            var response = await clientOne.PutAsync( $"{App.BackendUrl}/{Route + item.Id}", cnt);
            var result = response.IsSuccessStatusCode;
            clientOne.Dispose();
            return result;
        }
    }
}
