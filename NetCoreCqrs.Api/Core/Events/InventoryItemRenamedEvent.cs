using System;

namespace NetCoreCqrs.Api.Core.Events
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

