 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    public class MockDataStore : IDataStore<Trip>
    {
        List<Trip> items;

        private static MockDataStore _dataStore;
        public static MockDataStore GetInstance()
        {
            if (_dataStore == null)
                _dataStore = new MockDataStore();
            return _dataStore;
        }

        protected MockDataStore()
        {
            items = new List<Trip>();
            var _items = new List<Trip>
            {
                new Trip { Id = Guid.NewGuid().ToString(), Name = "Italien", Description="This is a nice description", Places = new List<IPlace>
                    {
                        new CampingPlace{Name = "Futzi und Emma", Description ="No fresh water", Location  = new Location{

                                Longitude = 4,
                                Latitude = 55,
                            }
                        },
                        new Restaurant{Name = "Denn's in", Description = "Funny little room", Location = new Location{
                                Longitude = 11,
                                Latitude = 11,
                            }
                        },
                        new NicePlace{ Name = "Waterfall", Description ="Awesome Waterfall", Location = new Location{
                                Longitude = 11,
                                Latitude = 22,
                            }
                        }
                    }
                },
                //new Trip { Id = Guid.NewGuid().ToString(), Name = "Second item", Description="This is a nice description"},
                //new Trip { Id = Guid.NewGuid().ToString(), Name = "Third item", Description="This is a nice description"},
                //new Trip { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Description="This is a nice description"},
                //new Trip { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Description="This is a nice description"},
                //new Trip { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Description="This is a nice description"},
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

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Trip arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Trip> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Trip>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
