using NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch;
using NetCoreCqrs.Domain.Inventory.Events;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory.ForDetails
{
    public sealed class InventoryItemCreatedEventHandler : IDomainEventHandler<InventoryItemCreatedEvent>
    {
        public Task HandleAsync(InventoryItemCreatedEvent message)
        {
            FakeReadDatabase.InventoryItemsDetailsMaterialisedView.Add(message.Id, new InventoryItemDetailsDto(message.Id, message.Name, 0, 0));
            return Task.CompletedTask;
        }
    }
}
