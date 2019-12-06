using System;

namespace NetCoreCqrs.Api.Core.Commands
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
