using NetCoreCqrs.Api.Core.Events;
using NetCoreCqrs.Api.Core.FakeBus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCqrs.Api.Core.EventStore
{
    public sealed class EventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;

        public EventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            // try to get event descriptors list for given aggregate id
            // otherwise -> create empty dictionary
            if (!FakeWriteDatabase.events.TryGetValue(aggregateId, out var eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                FakeWriteDatabase.events.TryAdd(aggregateId, eventDescriptors);
            }
            lock (eventDescriptors){
                // check whether latest event version matches current aggregate version
                // otherwise -> throw exception
                if (expectedVersion != -1 && eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion)
                {
                    throw new ConcurrencyException();
                }
                var i = expectedVersion;

                // iterate through current aggregate events increasing version with each processed event
                foreach (var @event in events)
                {
                    i++;
                    @event.Version = i;

                    // push event to the event descriptors list for current aggregate
                    eventDescriptors.Add(new EventDescriptor(aggregateId, @event, i));

                    // publish current event to the bus for further processing by subscribers
                    _publisher.Publish(@event);
                }
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            if (!FakeWriteDatabase.events.TryGetValue(aggregateId, out var eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }
            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }

        public List<Event> GetEventsForAggregate(Guid aggregateId, int startVersion)
        {
            return GetEventsForAggregate(aggregateId).Where(x => x.Version >= startVersion).ToList();
        }

        public int GetLastEventVersionForAggregate(Guid aggregateId)
        {
            return GetEventsForAggregate(aggregateId).Last().Version;
        }
    }
}
