using NetCoreCqrs.Api.Core.Commands;

namespace NetCoreCqrs.Api.Core.FakeBus
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;
    }
}
