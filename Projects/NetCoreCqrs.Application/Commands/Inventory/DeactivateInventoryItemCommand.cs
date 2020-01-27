using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using System;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public sealed class DeactivateInventoryItemCommand : ICommand
    {
        public Guid InventoryItemId { get; }
        public int OriginalVersion { get; }

        public DeactivateInventoryItemCommand(Guid inventoryItemId, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            OriginalVersion = originalVersion;
        }
    }
}
