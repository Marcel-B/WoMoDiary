using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;

namespace com.b_velop.WoMoDiary.Services
{
    public class MockTripDataStore : IDataStore<Trip>
    {
        public readonly List<Trip> Trips;
        public static Guid FirstTrip = Guid.NewGuid();
        public static Guid SecondTrip = Guid.NewGuid();

        public MockTripDataStore()
        {
            var userStore = ServiceLocator.Instance.Get<IDataStore<User>>() as MockUserDataStore;
            var user = userStore.Users[0];
            Trips = new List<Trip>();
            var firstTrip = new Trip
            {
                Id = FirstTrip,
                User = user,
                Name = "Italien",
                Description = "This is a nice description",
                Created = DateTimeOffset.Now,
                UserId = MockUserDataStore.FirstUserId
            };
            var secondTrip = new Trip
            {
                Id = SecondTrip,
                Name = "Holland",
                User = user,
                Description = "This is a nice description",
                Created = DateTimeOffset.Now,
                UserId = MockUserDataStore.FirstUserId
            };
            user.Trips.Add(firstTrip);
            user.Trips.Add(secondTrip);
            Trips.Add(firstTrip);
            Trips.Add(secondTrip);
        }

        public async Task<Trip> AddItemAsync(Trip item)
            => await Task.Run(() =>
            {
                Trips.Add(item);
                return item;
            });

        public async Task<Trip> UpdateItemAsync(Trip item)
        {
            var tmpItem = await Task.Run(() => Trips.SingleOrDefault(i => i.Id == item.Id));
            if (tmpItem != null)
                Trips.Remove(tmpItem);
            Trips.Add(item);
            return item;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await Task.Run(() => Trips.SingleOrDefault(i => i.Id == id));
            Trips.Remove(item);
            return true;
        }

        public async Task<Trip> GetItemAsync(Guid id)
            => await Task.FromResult(Trips.FirstOrDefault(s => s.Id == id));

        public async Task<IEnumerable<Trip>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(Trips);

        public async Task<IEnumerable<Trip>> GetItemsByFkAsync(Guid fk)
        {
            var result = await Task.Run(() => Trips.Where(t => t.UserId == fk));
            return result;
        }

        public async Task<IEnumerable<Trip>> GetItemsAsync(Guid id, bool forceRefresh = false)
            => await Task.Run(() => Trips.Where(t => t.User.Id == id));

        public Task<Trip> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
