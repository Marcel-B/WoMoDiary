using System.Collections.Generic;
using WoMoDiary.Domain;

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
