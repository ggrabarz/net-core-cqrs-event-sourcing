using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.CommandHandlers
{
    public sealed class RenameInventoryItemCommandHandler : ICommandHandler<RenameInventoryItemCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public RenameInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(RenameInventoryItemCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _repository.Save(item, message.OriginalVersion);
        }
    }
}
