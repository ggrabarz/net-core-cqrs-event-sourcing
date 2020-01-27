using NetCoreCqrs.Domain.BuildingBlocks;
using System;

namespace NetCoreCqrs.Domain.Inventory.Events
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

