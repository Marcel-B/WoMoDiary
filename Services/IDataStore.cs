using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public interface IDataStore<T> // where T : IItem
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsync(Guid id, bool forceRefresh = false);
    }
}
