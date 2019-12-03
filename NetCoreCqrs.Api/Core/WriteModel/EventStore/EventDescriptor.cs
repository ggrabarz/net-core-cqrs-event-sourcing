using NetCoreCqrs.Api.Core.Events;
using System;

namespace NetCoreCqrs.Api.Core.EventStore
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
