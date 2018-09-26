using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    public class MockTripDataStore : IDataStore<Trip>
    {
        private readonly List<Trip> _items;

        public MockTripDataStore()
        {
            _items = new List<Trip>
            {
                new Trip {
                    Id = App.FirstTrip,
                    Name = "Italien",
                    Description="This is a nice description",
                    Created = DateTimeOffset.Now,
                },
                new Trip {
                    Id = App.SecondTrip,
                    Name = "Holland",
                    Description="This is a nice description",
                    Created = DateTimeOffset.Now,
                },
            };
        }

        public async Task<bool> AddItemAsync(Trip item)
            => await Task.Run(() =>
            {
                _items.Add(item);
                return true;
            });

        public async Task<bool> UpdateItemAsync(Trip item)
        {
            var tmpItem = await Task.Run(() => _items.SingleOrDefault(i => i.Id == item.Id));
            if (tmpItem != null)
                _items.Remove(tmpItem);
            _items.Add(item);
            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await Task.Run(() => _items.SingleOrDefault(i => i.Id == id));
            _items.Remove(item);
            return true;
        }

        public async Task<Trip> GetItemAsync(Guid id)
            => await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));

        public async Task<IEnumerable<Trip>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(_items);

        public Task<IEnumerable<Trip>> GetItemsAsync(Guid id, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
