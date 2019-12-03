using System;

namespace NetCoreCqrs.Api.Core.Events
{
    public sealed class InventoryItemDeactivatedEvent : Event
    {
        public Guid Id { get; }

        public InventoryItemDeactivatedEvent(Guid id)
        {
            Id = id;
        }
    }
}

