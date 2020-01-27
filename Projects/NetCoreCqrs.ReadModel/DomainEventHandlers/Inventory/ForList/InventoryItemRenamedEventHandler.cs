using NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch;
using NetCoreCqrs.Domain.Inventory.Events;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory.ForList
{
    public sealed class InventoryItemRenamedEventHandler : IDomainEventHandler<InventoryItemRenamedEvent>
    {
        public Task HandleAsync(InventoryItemRenamedEvent message)
        {
            var item = FakeReadDatabase.InventoryItemsMaterialisedView.Find(x => x.Id == message.Id);
            item.Name = message.NewName;
            return Task.CompletedTask;
        }
    }
}
