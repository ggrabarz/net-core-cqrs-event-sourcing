using System;

namespace NetCoreCqrs.Api.Core.Events
{
    public sealed class ItemsRemovedFromInventoryEvent : Event
    {
        public Guid Id { get; }
        public int Count { get; }

        public ItemsRemovedFromInventoryEvent(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}

