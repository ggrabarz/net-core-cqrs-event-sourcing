using NetCoreCqrs.Api.Core.Domain;
using NetCoreCqrs.Api.Core.WriteModel.RepositoryCache;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace NetCoreCqrs.Api.Core.WriteModel.RepositorySnapshotCache
{
    public sealed class RepositorySnapshotCache<T> : IRepositorySnapshotCache<T> where T : AggregateRoot
    {
        private readonly ConcurrentDictionary<string, string> _cacheMemory = new ConcurrentDictionary<string, string>();
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() { ContractResolver = new PrivateWritableContractResolver() };

        public T GetOrDefault(string key)
        {
            return _cacheMemory.TryGetValue(key, out var result) ? JsonConvert.DeserializeObject<T>(result, _jsonSettings) : null;
        }

        public void Set(string key, T value)
        {
            if (value.Version % 5 != 0 || value.Version == 0) return;
            var cachedVersion = GetOrDefault(key)?.Version ?? -1;
            if (cachedVersion >= value.Version)
            {
                return;
            }
            _cacheMemory[key] = JsonConvert.SerializeObject(value, _jsonSettings);
        }
    }
}
