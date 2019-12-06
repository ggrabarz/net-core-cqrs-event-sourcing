using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.CommandHandlers
{
    public sealed class CheckInItemsToInventoryCommandHandler : ICommandHandler<CheckInItemsToInventoryCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public CheckInItemsToInventoryCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(CheckInItemsToInventoryCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.CheckIn(message.Count);
            _repository.Save(item, message.OriginalVersion);
        }
    }
}
