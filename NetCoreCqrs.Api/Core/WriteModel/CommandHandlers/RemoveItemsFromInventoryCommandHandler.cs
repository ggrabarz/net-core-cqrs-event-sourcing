using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.CommandHandlers
{
    public sealed class RemoveItemsFromInventoryCommandHandler : ICommandHandler<RemoveItemsFromInventoryCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public RemoveItemsFromInventoryCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(RemoveItemsFromInventoryCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Remove(message.Count);
            _repository.Save(item, message.OriginalVersion);
        }
    }
}
