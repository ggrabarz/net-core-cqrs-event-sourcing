using NetCoreCqrs.Api.Core.EventStore;
using System;

namespace NetCoreCqrs.Api.Core.Domain
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var result = new T();
            var eventsList = _storage.GetEventsForAggregate(id);
            result.LoadsFromHistory(eventsList);
            return result;
        }
    }
}
