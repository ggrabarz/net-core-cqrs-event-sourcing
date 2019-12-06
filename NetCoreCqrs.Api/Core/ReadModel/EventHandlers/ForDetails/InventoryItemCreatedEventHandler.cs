using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForDetails
{
    public sealed class InventoryItemCreatedEventHandler : IEventHandler<InventoryItemCreatedEvent>
    {
        public void Handle(InventoryItemCreatedEvent message)
        {
            FakeReadDatabase.InventoryItemsDetailsMaterialisedView.Add(message.Id, new InventoryItemDetailsDto(message.Id, message.Name, 0, 0));
        }
    }
}
