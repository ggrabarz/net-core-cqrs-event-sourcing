using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForList
{
    public sealed class InventoryItemCreatedEventHandler : IEventHandler<InventoryItemCreatedEvent>
    {
        public void Handle(InventoryItemCreatedEvent message)
        {
            FakeReadDatabase.InventoryItemsMaterialisedView.Add(new InventoryItemListDto(message.Id, message.Name));
        }
    }
}
