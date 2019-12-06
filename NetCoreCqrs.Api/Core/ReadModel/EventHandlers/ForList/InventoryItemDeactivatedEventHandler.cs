using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForList
{
    public sealed class InventoryItemDeactivatedEventHandler : IEventHandler<InventoryItemDeactivatedEvent>
    {
        public void Handle(InventoryItemDeactivatedEvent message)
        {
            FakeReadDatabase.InventoryItemsMaterialisedView.RemoveAll(x => x.Id == message.Id);
        }
    }
}
