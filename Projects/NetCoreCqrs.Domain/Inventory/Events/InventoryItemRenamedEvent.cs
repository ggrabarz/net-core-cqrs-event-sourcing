using NetCoreCqrs.Domain.BuildingBlocks;
using System;

namespace NetCoreCqrs.Domain.Inventory.Events
{
    public sealed class InventoryItemRenamedEvent : Event
    {
        public Guid Id { get; }
        public string NewName { get; }

        public InventoryItemRenamedEvent(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}

