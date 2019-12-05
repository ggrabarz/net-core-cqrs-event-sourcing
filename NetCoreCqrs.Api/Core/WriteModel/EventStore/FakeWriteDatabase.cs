using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.EventStore
{
    public class FakeWriteDatabase
    {
        public static ConcurrentDictionary<Guid, List<EventDescriptor>> events = new ConcurrentDictionary<Guid, List<EventDescriptor>>();
    }
}
