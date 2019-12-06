using System;

namespace NetCoreCqrs.Api.Core.Commands
{
    public sealed class CreateInventoryItemCommand : ICommand
    {
        public Guid InventoryItemId { get; }
        public string Name { get; }

        public CreateInventoryItemCommand(Guid inventoryItemId, string name)
        {
            InventoryItemId = inventoryItemId;
            Name = name;
        }
    }
}
