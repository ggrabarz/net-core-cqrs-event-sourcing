using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.CommandHandlers
{
    public sealed class DeactivateInventoryItemCommandHandler : ICommandHandler<DeactivateInventoryItemCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public DeactivateInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(DeactivateInventoryItemCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, message.OriginalVersion);
        }
    }
}
