using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class MockPlaceDataStore : IDataStore<Place>
    {
        Dictionary<Guid, Place> places;
        public MockPlaceDataStore()
        {
            places = new Dictionary<Guid, Place>();
            Place place = new CampingPlace
            {
                Id = Guid.NewGuid(),
                Name = "Futzi und Emma",
                Description = "No fresh water",
                Longitude = 4,
                Latitude = 55,
                TripFk = App.FirstTrip
            };
            places[place.Id] = place;
            place = new Restaurant
            {
                Id = Guid.NewGuid(),
                TripFk = App.FirstTrip,
                Name = "Denn's in",
                Description = "Funny little room",
                Longitude = 11,
                Latitude = 11
            };
            places[place.Id] = place;
            place = new NicePlace
            {
                Id = Guid.NewGuid(),
                TripFk = App.SecondTrip,
                Name = "Waterfall",
                Description = "Awesome Waterfall",
                Longitude = 11,
                Latitude = 22,
            };
            places[place.Id] = place;
        }

        public async Task<bool> AddItemAsync(Place item)
        {
            var result = await Task.Run(() => { places[item.Id] = item; return true; });
            return result;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var result = await Task.Run(() => { places.Remove(id); return true; });
            return result;
        }

        public async Task<Place> GetItemAsync(Guid id)
        {
            var result = await Task.Run(() => places[id]);
            return result;
        }

        public async Task<IEnumerable<Place>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await Task.Run(() =>
            {
                var ret = new List<Place>();
                foreach (var place in places.Values)
                {
                    ret.Add(place);
                }
                return ret;
            });
            return result;
        }

        public async Task<bool> UpdateItemAsync(Place item)
        {
            await Task.Run(() => places[item.Id] = item);
            return true;
        }
    }
}
