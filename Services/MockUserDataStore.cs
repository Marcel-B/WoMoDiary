using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class MockUserDataStore : IDataStore<User>
    {
        public static Guid FirstUserId = Guid.Parse("569dd649-f9f8-4990-b31b-45d43dda82c2");
        public static Guid SecondUserId = Guid.NewGuid();
        public IList<User> Users;
        public MockUserDataStore()
        {
            Users = new List<User>
            {
                new User{
                    UserId = FirstUserId,
                    Created = DateTimeOffset.Now,
                    Name = "Harry"
                }
            };
            App.User = Users[0];
        }

        public async Task<User> AddItemAsync(User item)
        {
            await Task.Run(() => Users.Add(item));
            return item;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
            => await Task.Run(() => Users.Remove(Users.SingleOrDefault(u => u.Id == id)));

        public async Task<User> GetItemAsync(Guid id)
            => await Task.Run(() => Users.SingleOrDefault(u => u.Id == id));

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
            => await Task.Run(() => Users);

        public Task<IEnumerable<User>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetItemsAsync(Guid id, bool forceRefresh = false)
            => await Task.Run(() => Users.Where(u => u.Id == id));

        public async Task<User> UpdateItemAsync(User item)
        {
            var user = await Task.Run(() => Users.SingleOrDefault(u => u.Id == item.Id));
            Users.Remove(user);
            Users.Add(item);
            return item;
        }
    }
}
