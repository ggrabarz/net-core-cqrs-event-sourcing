using NetCoreCqrs.Api.Core.EventStore;
using NetCoreCqrs.Api.Core.WriteModel.RepositoryCache;
using System;

namespace NetCoreCqrs.Api.Core.Domain
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _storage;
        private readonly IRepositoryCache<T> _repositoryCache;

        public Repository(IEventStore storage, IRepositoryCache<T> repositoryCache)
        {
            _storage = storage;
            _repositoryCache = repositoryCache;
        }

        public void Save(T aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
            aggregate.MarkChangesAsCommitted();
            _repositoryCache.Set(aggregate.Id.ToString(), aggregate);
        }

        public T GetById(Guid id)
        {
            var cachedValue = _repositoryCache.GetOrDefault(id.ToString());
            if(cachedValue != null && cachedValue.Version == _storage.GetLastEventVersionForAggregate(id))
            {
                _repositoryCache.Refresh(id.ToString());
                return cachedValue;
            }
            var result = new T();
            var eventsList = _storage.GetEventsForAggregate(id);
            result.LoadsFromHistory(eventsList);
            return result;
        }
    }
}
