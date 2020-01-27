using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Domain.Inventory;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public sealed class CheckInItemsToInventoryCommandHandler : ICommandHandler<CheckInItemsToInventoryCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public CheckInItemsToInventoryCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(CheckInItemsToInventoryCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.CheckIn(message.Count);
            _repository.Save(item, message.OriginalVersion);
            return Task.CompletedTask;
        }
    }
}
