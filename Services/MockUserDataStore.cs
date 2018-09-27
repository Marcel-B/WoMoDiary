using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using System.Linq;

namespace WoMoDiary.Services
{
    public class MockUserDataStore : IDataStore<User>
    {
        public static Guid FirstUserId = Guid.Parse("569dd649-f9f8-4990-b31b-45d43dda82c2");
        public static Guid SecondUserId = Guid.NewGuid();
        private readonly IList<User> _users;
        public MockUserDataStore()
        {
            _users = new List<User>
            {
                new User{
                    Id = FirstUserId,
                    Created = DateTimeOffset.Now,
                    Name = "Harry"
                }
            };
        }

        public async Task<bool> AddItemAsync(User item)
        {
            await Task.Run(() => _users.Add(item));
            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
            => await Task.Run(() => _users.Remove(_users.SingleOrDefault(u => u.Id == id)));

        public async Task<User> GetItemAsync(Guid id)
            => await Task.Run(() => _users.SingleOrDefault(u => u.Id == id));

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
            => await Task.Run(() => _users);

        public async Task<IEnumerable<User>> GetItemsAsync(Guid id, bool forceRefresh = false)
            => await Task.Run(() => _users.Where(u => u.Id == id));

        public async Task<bool> UpdateItemAsync(User item)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(u => u.Id == item.Id));
            _users.Remove(user);
            _users.Add(item);
            return true;
        }
    }
}
