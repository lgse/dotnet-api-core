using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace API.Core.Cache
{
    public interface ICacheProvider
    {
        public Task<bool> StoreAsync<T>(string key, T obj, TimeSpan? expiry = null);

        public Task<bool> StoreMultipleAsync<T>(Dictionary<string, T> objects);

        public Task<T?> RetrieveAsync<T>(string key);

        Task<List<T>> RetrieveMultipleAsync<T>(IEnumerable<string> keys);

        public Task DeleteAsync(string key);
    }
}
