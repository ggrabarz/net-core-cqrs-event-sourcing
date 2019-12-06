using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.CommandHandlers
{
    public class CreateInventoryItemCommandHandler : ICommandHandler<CreateInventoryItemCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public CreateInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateInventoryItemCommand message)
        {
            var item = new InventoryItem(message.InventoryItemId, message.Name);
            _repository.Save(item, -1);
        }
    }
}
