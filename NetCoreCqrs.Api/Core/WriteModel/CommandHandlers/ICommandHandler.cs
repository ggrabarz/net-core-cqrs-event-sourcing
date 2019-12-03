using NetCoreCqrs.Api.Core.Commands;

namespace NetCoreCqrs.Api.Core.CommandHandlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T message);
    }
}
