using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace WoMoDiary.Services
{
    public class UserDataStore : IDataStore<User>
    {
        public string BaseUrl { get; set; }

        public UserDataStore()
        {
            BaseUrl = "https://womo.marcelbenders.de/";
        }

        protected  string Route
        {
            get => "api/login/";
        }
        protected  string RouteSpecial
        {
            get => "api/login/byusername/";
        }

        public async Task<bool> AddItemAsync(User item)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            var obj = JsonConvert.SerializeObject(item);
            var stringContent = new StringContent(obj,Encoding.UTF8, "application/json");
            await httpClient.PostAsync($"api/login", stringContent);
            httpClient.Dispose();
            return true;
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            return null;
        }
        public async Task<User> GetItemAsync(Guid id)
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync($"https://womo.marcelbenders.de/api/login/byusername/{id}");
            var body = await request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(body);
            httpClient.Dispose();
            return user;
        }

        public Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return null;
        }

        public async Task<User> GetItemsAsync(Guid id, bool forceRefresh = false)
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync($"https://womo.marcelbenders.de/api/login/byusername/{id}");
            var body = await request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(body);
            httpClient.Dispose();
            return user;
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            var httpClient = new HttpClient();
            var obj = JsonConvert.SerializeObject(item);
            var stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            await httpClient.PutAsync($"https://womo.marcelbenders.de/api/login/{item.UserId}", stringContent);
            httpClient.Dispose();
            return true;
        }

        Task<IEnumerable<User>> IDataStore<User>.GetItemsAsync(Guid id, bool forceRefresh)
        {
            throw new NotImplementedException();
        }
    }
}
