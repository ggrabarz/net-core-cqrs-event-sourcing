using NetCoreCqrs.Domain.BuildingBlocks;
using System;

namespace NetCoreCqrs.Domain.Inventory.Events
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

