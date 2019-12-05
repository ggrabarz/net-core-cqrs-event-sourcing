using NetCoreCqrs.Api.Core.Events;
using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.EventStore
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
        List<Event> GetEventsForAggregate(Guid aggregateId, int startVersion);
        int GetLastEventVersionForAggregate(Guid aggregateId);
    }
}
