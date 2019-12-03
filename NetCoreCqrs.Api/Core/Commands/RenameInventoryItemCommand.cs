using System;

namespace NetCoreCqrs.Api.Core.Commands
{
    public sealed class RenameInventoryItemCommand : ICommand
    {
        public Guid InventoryItemId { get; }
        public string NewName { get; }
        public int OriginalVersion { get; }

        public RenameInventoryItemCommand(Guid inventoryItemId, string newName, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            NewName = newName;
            OriginalVersion = originalVersion;
        }
    }
}
