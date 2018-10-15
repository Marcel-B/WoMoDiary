using WoMoDiary.Domain;
using System.Collections.ObjectModel;
using System;

namespace WoMoDiary.Services
{
    public class AppStore
    {
        static readonly Lazy<AppStore> instance = new Lazy<AppStore>(() => new AppStore());
        public static AppStore Instance => instance.Value;

        protected AppStore()
        {
            Trips = new ObservableCollection<Trip>();
            Places = new ObservableCollection<Place>();
        }

        public User User { get; set; }
        public Trip CurrentTrip { get; set; }
        public Place CurrentPlace { get; set; }
        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<Place> Places { get; set; }
    }
}
