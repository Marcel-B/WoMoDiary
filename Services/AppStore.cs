using System;
using WoMoDiary.Domain;
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

        public TripOtd CurrentTrip { get; set; }
        public Location CurrentLocation { get; set; }


    }
}
