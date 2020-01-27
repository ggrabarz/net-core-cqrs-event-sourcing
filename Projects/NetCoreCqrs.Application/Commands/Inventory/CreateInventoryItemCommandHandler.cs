using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Domain.Inventory;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.Commands.Inventory
{
    public class CreateInventoryItemCommandHandler : ICommandHandler<CreateInventoryItemCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public CreateInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(CreateInventoryItemCommand message)
        {
            var item = new InventoryItem(message.InventoryItemId, message.Name);
            _repository.Save(item, -1);
            return Task.CompletedTask;
        }
    }
}
