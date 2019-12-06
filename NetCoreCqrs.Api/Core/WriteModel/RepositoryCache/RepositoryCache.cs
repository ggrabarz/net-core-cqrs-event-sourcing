using NetCoreCqrs.Api.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.WriteModel.RepositoryCache
{
    public sealed class RepositoryCache<T> : IRepositoryCache<T> where T : AggregateRoot
    {
        private readonly ConcurrentDictionary<string, CacheTuple<string>> _cacheMemory = new ConcurrentDictionary<string, CacheTuple<string>>();
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() { ContractResolver = new PrivateWritableContractResolver() };

        public T GetOrDefault(string key)
        {
            return _cacheMemory.TryGetValue(key, out var result) && result.Expiration > DateTime.UtcNow ? JsonConvert.DeserializeObject<T>(result.Value, _jsonSettings) : null;
        }

        public void Refresh(string key)
        {
            if(_cacheMemory.TryGetValue(key, out var result) && result.Expiration > DateTime.UtcNow)
            {
                result.Expiration = DateTime.UtcNow.AddMinutes(2);
            }
        }

        public void Remove(string key)
        {
            _cacheMemory.Remove(key, out var _);
        }

        public void Set(string key, T value)
        {
            var cachedVersion = GetOrDefault(key)?.Version ?? -1;
            if(cachedVersion >= value.Version)
            {
                return;
            }
            var cachedElement = new CacheTuple<string>()
            {
                Value = JsonConvert.SerializeObject(value, _jsonSettings),
                Expiration = DateTime.UtcNow.AddMinutes(2)
            };
            _cacheMemory[key] = cachedElement;
        }
    }
}
