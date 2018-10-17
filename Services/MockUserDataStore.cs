using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class MockUserDataStore : IDataStore<User>
    {
        public const string USER_ID = "2c3facaef-14ef-4cc8-874f-0f9917082959";
        public static Guid FirstUserId = Guid.Parse("569dd649-f9f8-4990-b31b-45d43dda82c2");
        public static Guid SecondUserId = Guid.NewGuid();
        public IList<User> Users;
        public string hash = "EwhBW1+iwMaT87Y6uhrMIbDlLwMBAVGGT+8ARWiTFwENIyy9caVxJqYXoMZGjw5n/lCF+rkzab+t5CAr0vypIQ==";
        public string salt = "k8kfMkWKx7RMMXVcrIfDqAIpwYhVqiXpOdy+unP5Xvjf14NSmLyhIJ13OXyvDv5mIEyM05ly4QZ8S+Wq9eNL4uLuEEAAf/KLwaaSN6dkgkm6rR0j39WmkR5r55iJV7EnG0WAXo5SnWL3jOD2+h44V1iqbI+FgMXM6V/BX1+Fg9Y=";
        public MockUserDataStore()
        {
            Users = new List<User>
            {
                new User{
                    UserId = FirstUserId,
                    Created = DateTimeOffset.Now.AddDays(-5),
                    LastEdit = DateTimeOffset.Now,
                    Name = "Testuser",
                    Hash = Convert.FromBase64String(hash),
                    Salt = Convert.FromBase64String(salt)
                }
            };
            //App.User = Users[0];
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
            return null;
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

        public async Task<User> GetByName(string name)
        {
            var user = await Task.Run(() => Users.Single(u => u.Name == name));
            return user;
        }
    }
}
