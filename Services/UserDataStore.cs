using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.WoMoDiary.Domain;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace com.b_velop.WoMoDiary.Services
{
    public class UserDataStore : CloudDataStore<User>
    {
        public UserDataStore()
        {
            Items = new List<User>();
        }

        protected override string Route => "api/user/";
        protected override string RouteSpecial => "api/user/byusername/";

        public override Task<IEnumerable<User>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }

        public override async Task<User> GetByName(string username)
        {
            if (!CrossConnectivity.Current.IsConnected) return null;
            try
            {
                var response = await Client.GetAsync(RouteSpecial + username);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(content);
            }
            catch (Exception ex)
            {
                App.LogOutLn(ex.StackTrace, GetType().Name);
                return null;
            }
        }
    }
}
