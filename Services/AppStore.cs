using System.Collections.Generic;
using WoMoDiary.Domain;
using System;
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

        protected AppStore() { Trips = new ObservableCollection<Trip>(); }

        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Trip CurrentTrip { get; set; }
        public Place CurrentPlace { get; set; }
        public ObservableCollection<Trip> Trips { get; set; }

    }
}
