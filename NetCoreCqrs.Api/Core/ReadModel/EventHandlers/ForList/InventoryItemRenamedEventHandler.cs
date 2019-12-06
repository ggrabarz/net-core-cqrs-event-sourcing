using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers.ForList
{
    public sealed class InventoryItemRenamedEventHandler : IEventHandler<InventoryItemRenamedEvent>
    {
        public void Handle(InventoryItemRenamedEvent message)
        {
            var item = FakeReadDatabase.InventoryItemsMaterialisedView.Find(x => x.Id == message.Id);
            item.Name = message.NewName;
        }
    }
}
