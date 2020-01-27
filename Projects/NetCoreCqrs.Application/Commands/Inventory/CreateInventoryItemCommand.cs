using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using System;

namespace NetCoreCqrs.Application.Commands.Inventory
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
