using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using System;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public sealed class RemoveItemsFromInventoryCommand : ICommand
    {
        public Guid InventoryItemId { get; }
        public int Count { get; }
        public int OriginalVersion { get; }

        public RemoveItemsFromInventoryCommand(Guid inventoryItemId, int count, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
            OriginalVersion = originalVersion;
        }
    }
}
