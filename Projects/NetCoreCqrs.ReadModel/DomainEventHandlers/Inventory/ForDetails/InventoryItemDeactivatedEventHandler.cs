using NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch;
using NetCoreCqrs.Domain.Inventory.Events;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory.ForDetails
{
    public sealed class InventoryItemDeactivatedEventHandler : IDomainEventHandler<InventoryItemDeactivatedEvent>
    {
        public Task HandleAsync(InventoryItemDeactivatedEvent message)
        {
            FakeReadDatabase.InventoryItemsDetailsMaterialisedView.Remove(message.Id);
            return Task.CompletedTask;
        }
    }
}
