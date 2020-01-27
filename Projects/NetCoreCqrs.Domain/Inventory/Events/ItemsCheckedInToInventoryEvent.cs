using NetCoreCqrs.Domain.BuildingBlocks;
using System;

namespace NetCoreCqrs.Domain.Inventory.Events
{
    public sealed class ItemsCheckedInToInventoryEvent : Event
    {
        public Guid Id { get; }
        public int Count { get; }

        public ItemsCheckedInToInventoryEvent(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}

