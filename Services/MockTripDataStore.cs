using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    public class MockTripDataStore : IDataStore<Trip>
    {
        List<Trip> items;

        public MockTripDataStore()
        {
            items = new List<Trip>();
            var _items = new List<Trip>
            {
                new Trip { Id = App.FirstTrip, Name = "Italien", Description="This is a nice description"}
            };

            foreach (Trip item in _items)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Trip item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Trip item)
        {
            var _item = items.Where((Trip arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var _item = items.Where((Trip arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Trip> GetItemAsync(Guid id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Trip>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
