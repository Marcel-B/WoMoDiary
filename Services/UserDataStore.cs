using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class UserDataStore : CloudDataStore<User>
    {
        public UserDataStore()
        {
            Items = new List<User>();
        }

        protected override string Route => "api/login/";
        protected override string RouteSpecial => "api/user/byusername/";

        public override Task<IEnumerable<User>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }
    }
}
