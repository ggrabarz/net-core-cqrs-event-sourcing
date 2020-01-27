using NetCoreCqrs.Domain.BuildingBlocks;
using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Persistence.EventStore
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
        List<Event> GetEventsForAggregate(Guid aggregateId, int startVersion);
        int GetLastEventVersionForAggregate(Guid aggregateId);
    }
}
