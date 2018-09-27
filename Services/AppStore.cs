using System;
using WoMoDiary.Domain;
using System.Collections.Generic;
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

        protected AppStore() { Trips = new List<Trip>(); }

        public Guid UserId { get; set; }
        public Trip CurrentTrip { get; set; }
        public Place CurrentPlace { get; set; }
        public IList<Trip> Trips { get; set; }

    }
}
