using System;

namespace NetCoreCqrs.Api.Core.Commands
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
