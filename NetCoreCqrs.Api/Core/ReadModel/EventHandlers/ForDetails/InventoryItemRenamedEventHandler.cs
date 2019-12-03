using NetCoreCqrs.Api.Core.Events;
using System;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForDetails
{
    public sealed class InventoryItemRenamedEventHandler : IEventHandler<InventoryItemRenamedEvent>
    {
        public void Handle(InventoryItemRenamedEvent message)
        {
            var inventoryDetails = GetDetailsItem(message.Id);
            inventoryDetails.Name = message.NewName;
            inventoryDetails.Version = message.Version;
        }

        private InventoryItemDetailsDto GetDetailsItem(Guid id)
        {
            if (!FakeReadDatabase.InventoryItemsDetailsMaterialisedView.TryGetValue(id, out var result))
            {
                throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
            }
            return result;
        }
    }
}
