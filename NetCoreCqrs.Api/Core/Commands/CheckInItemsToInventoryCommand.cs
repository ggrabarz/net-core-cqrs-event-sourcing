using System;

namespace NetCoreCqrs.Api.Core.Commands
{
    public sealed class CheckInItemsToInventoryCommand : ICommand
    {
        public Guid InventoryItemId { get; }
        public int Count { get; }
        public int OriginalVersion { get; }

        public CheckInItemsToInventoryCommand(Guid inventoryItemId, int count, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
            OriginalVersion = originalVersion;
        }
    }
}
