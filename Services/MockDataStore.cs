 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary
{
    public class MockDataStore : IDataStore<TripOtd>
    {
        List<TripOtd> items;

        public MockDataStore()
        {
            items = new List<TripOtd>();
            var _items = new List<TripOtd>
            {
                new TripOtd { Id = Guid.NewGuid(), Name = "Italien", Description="This is a nice description", Places = new List<Place>
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
            };

            foreach (TripOtd item in _items)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(TripOtd item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TripOtd item)
        {
            var _item = items.Where((TripOtd arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var _item = items.Where((TripOtd arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<TripOtd> GetItemAsync(Guid id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TripOtd>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
