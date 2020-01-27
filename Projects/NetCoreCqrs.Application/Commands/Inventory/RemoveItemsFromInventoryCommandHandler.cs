using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Domain.Inventory;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public sealed class RemoveItemsFromInventoryCommandHandler : ICommandHandler<RemoveItemsFromInventoryCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public RemoveItemsFromInventoryCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(RemoveItemsFromInventoryCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Remove(message.Count);
            _repository.Save(item, message.OriginalVersion);
            return Task.CompletedTask;
        }
    }
}
