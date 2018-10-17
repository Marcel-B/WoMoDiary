using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using System.Linq;

namespace WoMoDiary.Services
{
    public class MockPlaceDataStore : IDataStore<Place>
    {
        private readonly IList<Place> _places;

        public MockPlaceDataStore()
        {
            var tripStore = ServiceLocator.Instance.Get<IDataStore<Trip>>() as MockTripDataStore;
            _places = new List<Place>{
            new CampingPlace
            {
                Id = Guid.NewGuid(),
                Name = "Emma's Camping",
                Description = "No water",
                Longitude = 11.39227126,
                Latitude = 43.94877581,
                Altitude = 186,
                Trip = tripStore.Trips[0],
                Rating = 0,
                Created = DateTimeOffset.Now,
            },
            new Restaurant
            {
                Id = Guid.NewGuid(),
                Trip = tripStore.Trips[0],
                Name = "Denn's in",
                Description = "Funny little restaurant",
                Longitude = 11.38875844,
                Latitude = 43.9535555,
                Altitude = 191,
                Rating = 2,
                Created = DateTimeOffset.Now,
            },
                new Poi
            {
                Id = Guid.NewGuid(),
                Trip = tripStore.Trips[1],
                TripId = tripStore.Trips[1].Id,
                Name = "Waterfall",
                Description = "Awesome Waterfall",
                Longitude = 8.16295347,
                Latitude = 47.46171372,
                Altitude = 347,
                Rating = 5,
                Created = DateTimeOffset.Now,
                }
            };
        }

        public async Task<Place> AddItemAsync(Place item)
            => await Task.Run(() =>
            {
                _places.Add(item);
                return item;
            });

        public async Task<bool> DeleteItemAsync(Guid id)
            => await Task.Run(() => { _places.Remove(_places.Single(p => p.Id == id)); return true; });

        public async Task<Place> GetItemAsync(Guid id)
            => await Task.Run(() => _places.Single(p => p.Id == id));

        public async Task<IEnumerable<Place>> GetItemsAsync(bool forceRefresh = false)
            => await Task.Run(() =>
            {
                var ret = new List<Place>();
                foreach (var place in _places)
                {
                    ret.Add(place);
                }
                return ret;
            });

        public async Task<IEnumerable<Place>> GetItemsByFkAsync(Guid fk)
            => await Task.Run(() => _places.Where(p => p.TripId == fk));

        public async Task<IEnumerable<Place>> GetItemsAsync(Guid id, bool forceRefresh = false)
            => await Task.Run(() => _places);

        /// <summary>
        /// Updates the item async.
        /// </summary>
        /// <returns>Success of operation</returns>
        /// <param name="item">A Place</param>
        public async Task<Place> UpdateItemAsync(Place item)
            => await Task.Run(() =>
            {
                var old = _places.SingleOrDefault(i => i.Id == item.Id);
                if (old != null)
                    _places.Remove(old);
                _places.Add(item);
                return item;
            });

        public async Task<Place> GetByName(string name)
            => await Task.Run(() => _places.SingleOrDefault(p => p.Name == name));
    }
}
