using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Domain.Inventory;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public sealed class RenameInventoryItemCommandHandler : ICommandHandler<RenameInventoryItemCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public RenameInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(RenameInventoryItemCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _repository.Save(item, message.OriginalVersion);
            return Task.CompletedTask;
        }
    }
}
