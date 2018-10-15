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
        protected HttpClient Client;
        protected IEnumerable<T> Items;
        protected abstract string Route { get; }
        protected abstract string RouteSpecial { get; }
        public abstract Task<IEnumerable<T>> GetItemsByFkAsync(Guid fk);

        protected CloudDataStore()
        {
            if (Client == null)
                Client = new HttpClient
                {
                    BaseAddress = new Uri($"{App.BACKEND_URL}/")
                };
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            try
            {
                if (item == null || !CrossConnectivity.Current.IsConnected)
                    return default(T);
                var serializedItem = JsonConvert.SerializeObject(item);
                var cnt = new StringContent(serializedItem, Encoding.UTF8, "application/json");
                var buffer = Encoding.UTF8.GetBytes(serializedItem);
                var byteContent = new ByteArrayContent(buffer);
                var response = await Client.PutAsync(Route + item.Id, cnt);
                var succ = response.IsSuccessStatusCode;
                var result = await response.Content.ReadAsStreamAsync();

                return response.IsSuccessStatusCode ? item : default(T);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(T);
            }
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            if (id != Guid.Empty && !CrossConnectivity.Current.IsConnected)
                return false;
            var response = await Client.DeleteAsync(Route + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            if (!CrossConnectivity.Current.IsConnected) return default(T);
            try
            {
                var response = await Client.GetAsync(Route + id);
                if (!response.IsSuccessStatusCode) return default(T);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(T);
            }

        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (!forceRefresh || !CrossConnectivity.Current.IsConnected) return Items;
            var json = await Client.GetStringAsync(Route);
            Items = JsonConvert.DeserializeObject<T[]>(json);
            return Items;
        }

        public async Task<T> AddItemAsync(T item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return default(T);
            try
            {
                var serializedItem = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(Route, content);
                return response.IsSuccessStatusCode ? item : default(T);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(T);
            }

        }
    }
}
