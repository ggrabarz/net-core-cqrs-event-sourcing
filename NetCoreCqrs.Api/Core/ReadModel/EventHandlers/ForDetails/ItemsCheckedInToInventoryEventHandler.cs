using NetCoreCqrs.Api.Core.Events;
using System;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForDetails
{
    public sealed class ItemsCheckedInToInventoryEventHandler : IEventHandler<ItemsCheckedInToInventoryEvent>
    {
        public void Handle(ItemsCheckedInToInventoryEvent message)
        {
            var inventoryDetails = GetDetailsItem(message.Id);
            inventoryDetails.CurrentCount += message.Count;
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
