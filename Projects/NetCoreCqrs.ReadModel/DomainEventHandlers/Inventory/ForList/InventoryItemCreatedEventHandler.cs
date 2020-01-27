using NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch;
using NetCoreCqrs.Domain.Inventory.Events;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory.ForList
{
    public sealed class InventoryItemCreatedEventHandler : IDomainEventHandler<InventoryItemCreatedEvent>
    {
        public Task HandleAsync(InventoryItemCreatedEvent message)
        {
            FakeReadDatabase.InventoryItemsMaterialisedView.Add(new InventoryItemListDto(message.Id, message.Name));
            return Task.CompletedTask;
        }
    }
}
