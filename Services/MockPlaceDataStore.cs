using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using System.Linq;

namespace WoMoDiary.Services
{
    public class MockPlaceDataStore : IDataStore<Place>
    {
        private readonly Dictionary<Guid, Place> _places;

        public MockPlaceDataStore()
        {
            _places = new Dictionary<Guid, Place>();
            Place place = new CampingPlace
            {
                Id = Guid.NewGuid(),
                Name = "Emma's Camping",
                Description = "No water",
                Longitude = 11.39227126,
                Latitude = 43.94877581,
                Altitude = 186,
                TripFk = App.FirstTrip,
                Rating = 0,
                Created = DateTimeOffset.Now,
            };
            _places[place.Id] = place;
            place = new Restaurant
            {
                Id = Guid.NewGuid(),
                TripFk = App.FirstTrip,
                Name = "Denn's in",
                Description = "Funny little restaurant",
                Longitude = 11.38875844,
                Latitude = 43.9535555,
                Altitude = 191,
                Rating = 2,
                Created = DateTimeOffset.Now,
            };
            _places[place.Id] = place;
            place = new NicePlace
            {
                Id = Guid.NewGuid(),
                TripFk = App.SecondTrip,
                Name = "Waterfall",
                Description = "Awesome Waterfall",
                Longitude = 8.16295347,
                Latitude = 47.46171372,
                Altitude = 347,
                Rating = 5,
                Created = DateTimeOffset.Now,
            };
            _places[place.Id] = place;
        }

        public async Task<bool> AddItemAsync(Place item)
            => await Task.Run(() => { _places[item.Id] = item; return true; });

        public async Task<bool> DeleteItemAsync(Guid id)
            => await Task.Run(() => { _places.Remove(id); return true; });

        public async Task<Place> GetItemAsync(Guid id)
            => await Task.Run(() => _places[id]);

        public async Task<IEnumerable<Place>> GetItemsAsync(bool forceRefresh = false)
            => await Task.Run(() =>
            {
                var ret = new List<Place>();
                foreach (var place in _places.Values)
                {
                    ret.Add(place);
                }
                return ret;
            });

        public async Task<IEnumerable<Place>> GetItemsAsync(Guid id, bool forceRefresh = false)
        => await Task.Run(() =>
        {
            var list = new List<Place>();
            foreach (var place in _places.Values)
            {
                if (place.TripFk == id)
                    list.Add(place);
            }
            return list;
        })    ;

        /// <summary>
        /// Updates the item async.
        /// </summary>
        /// <returns>Success of operation</returns>
        /// <param name="item">A Place</param>
        public async Task<bool> UpdateItemAsync(Place item)
            => await Task.Run(() =>
            {
                _places[item.Id] = item;
                return true;
            });
    }
}
