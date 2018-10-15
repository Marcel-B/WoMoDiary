using WoMoDiary.Domain;
using System.Collections.ObjectModel;

namespace WoMoDiary.Services
{
    public class AppStore
    {
        private static AppStore _instance;

        public static AppStore GetInstance()
        {
            if (_instance == null)
                _instance = new AppStore();
            return _instance;
        }

        protected AppStore() { }

        public User User { get; set; }
        public Trip CurrentTrip { get; set; }
        public Place CurrentPlace { get; set; }
    }
}
