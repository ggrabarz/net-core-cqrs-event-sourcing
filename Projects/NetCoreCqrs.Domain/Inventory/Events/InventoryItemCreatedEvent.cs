using NetCoreCqrs.Domain.BuildingBlocks;
using System;

namespace NetCoreCqrs.Domain.Inventory.Events
{
    public sealed class InventoryItemCreatedEvent : Event
    {
        public Guid Id { get; }
        public string Name { get; }

        public InventoryItemCreatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

