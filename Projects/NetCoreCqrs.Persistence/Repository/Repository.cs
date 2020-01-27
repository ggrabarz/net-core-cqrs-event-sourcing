using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Persistence.EventStore;
using NetCoreCqrs.Persistence.RepositoryCache;
using NetCoreCqrs.Persistence.RepositorySnapshotCache;
using System;

namespace NetCoreCqrs.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _storage;
        private readonly IRepositoryCache<T> _repositoryCache;
        private readonly IRepositorySnapshotCache<T> _repositorySnapshotCache;

        public Repository(IEventStore storage, IRepositoryCache<T> repositoryCache, IRepositorySnapshotCache<T> repositorySnapshotCache)
        {
            _storage = storage;
            _repositoryCache = repositoryCache;
            _repositorySnapshotCache = repositorySnapshotCache;
        }

        public void Save(T aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
            aggregate.MarkChangesAsCommitted();
            _repositoryCache.Set(aggregate.Id.ToString(), aggregate);
            _repositorySnapshotCache.Set(aggregate.Id.ToString(), aggregate);
        }

        public T GetById(Guid id)
        {
            var cachedValue = _repositoryCache.GetOrDefault(id.ToString());
            if (cachedValue != null && cachedValue.Version == _storage.GetLastEventVersionForAggregate(id))
            {
                _repositoryCache.Refresh(id.ToString());
                return cachedValue;
            }
            var snapshotValue = _repositorySnapshotCache.GetOrDefault(id.ToString());
            if (snapshotValue != null)
            {
                var eventsListForSnapshot = _storage.GetEventsForAggregate(id, snapshotValue.Version + 1);
                snapshotValue.LoadsFromHistory(eventsListForSnapshot);
                return snapshotValue;
            }
            var result = new T();
            var eventsList = _storage.GetEventsForAggregate(id);
            result.LoadsFromHistory(eventsList);
            return result;
        }
    }
}
