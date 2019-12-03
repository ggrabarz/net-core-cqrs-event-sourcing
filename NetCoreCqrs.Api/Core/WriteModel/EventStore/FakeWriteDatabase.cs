using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.EventStore
{
    public class FakeWriteDatabase
    {
        public static Dictionary<Guid, List<EventDescriptor>> events = new Dictionary<Guid, List<EventDescriptor>>();
    }
}
