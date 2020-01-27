using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Domain.Inventory;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public sealed class DeactivateInventoryItemCommandHandler : ICommandHandler<DeactivateInventoryItemCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public DeactivateInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(DeactivateInventoryItemCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, message.OriginalVersion);
            return Task.CompletedTask;
        }
    }
}
