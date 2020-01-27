using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var handlers = GetServices<ICommandHandler<TCommand>>(_serviceProvider);
            if (handlers != null && handlers.Any())
            {
                if (handlers.Count() != 1) throw new InvalidOperationException($"cannot send {typeof(TCommand).Name} to more than one handler");
                await handlers.First().HandleAsync(command);
            }
            else
            {
                throw new InvalidOperationException($"no handler registered for {typeof(TCommand).Name}");
            }
        }

        public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var handlers = GetServices<ICommandHandler<TCommand, TResult>>(_serviceProvider);
            if (handlers != null && handlers.Any())
            {
                if (handlers.Count() != 1) throw new InvalidOperationException($"cannot send {typeof(TCommand).Name} to more than one handler");
                return await handlers.First().HandleAsync(command);
            }
            else
            {
                throw new InvalidOperationException($"no handler registered for {typeof(TCommand).Name}");
            }
        }

        private static IEnumerable<T> GetServices<T>(IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            return (IEnumerable<T>)provider.GetService(typeof(IEnumerable<T>));
        }
    }
}
