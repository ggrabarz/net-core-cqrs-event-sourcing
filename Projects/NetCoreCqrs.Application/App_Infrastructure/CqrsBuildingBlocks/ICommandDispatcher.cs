using System.Threading.Tasks;

namespace NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
