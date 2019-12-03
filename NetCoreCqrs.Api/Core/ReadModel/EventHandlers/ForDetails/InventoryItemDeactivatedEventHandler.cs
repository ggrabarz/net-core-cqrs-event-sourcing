using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForDetails
{
    public sealed class InventoryItemDeactivatedEventHandler : IEventHandler<InventoryItemDeactivatedEvent>
    {
        public void Handle(InventoryItemDeactivatedEvent message)
        {
            FakeReadDatabase.InventoryItemsDetailsMaterialisedView.Remove(message.Id);
        }
    }
}
