using NetCoreCqrs.Domain.BuildingBlocks;
using System;

namespace NetCoreCqrs.Persistence.EventStore
{
    public struct EventDescriptor
    {

        public readonly Event EventData;
        public readonly Guid Id;
        public readonly int Version;

        public EventDescriptor(Guid id, Event eventData, int version)
        {
            EventData = eventData;
            Version = version;
            Id = id;
        }
    }
}
