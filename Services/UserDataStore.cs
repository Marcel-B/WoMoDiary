using WoMoDiary.Domain;
using System.Collections.Generic;

namespace WoMoDiary.Services
{
    public class UserDataStore : CloudDataStore<User>
    {
        public UserDataStore()
        {
            items = new List<User>();
        }

        protected override string Route
        {
            get => "api/user/";
        }
        protected override string RouteSpecial
        {
            get => "api/user/byusername/";
        }
    }
}
