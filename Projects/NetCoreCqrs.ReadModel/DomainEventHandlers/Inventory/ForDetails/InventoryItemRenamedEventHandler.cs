using NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch;
using NetCoreCqrs.Domain.Inventory.Events;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory.ForDetails
{
    public sealed class InventoryItemRenamedEventHandler : IDomainEventHandler<InventoryItemRenamedEvent>
    {
        public Task HandleAsync(InventoryItemRenamedEvent message)
        {
            var inventoryDetails = GetDetailsItem(message.Id);
            inventoryDetails.Name = message.NewName;
            inventoryDetails.Version = message.Version;
            return Task.CompletedTask;
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
