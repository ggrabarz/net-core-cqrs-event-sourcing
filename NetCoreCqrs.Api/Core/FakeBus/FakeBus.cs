using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Events;
using System;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCqrs.Api.Core.CommandHandlers;
using NetCoreCqrs.Api.Core.ReadModel.EventHandlers;
using NetCoreCqrs.Api.App_Infrastructure;

namespace NetCoreCqrs.Api.Core.FakeBus
{
    public class FakeBus : ICommandSender, IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public FakeBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Send<T>(T command) where T : ICommand
        {
            var handlers = _serviceProvider.GetServices(typeof(ICommandHandler<>).MakeGenericType(command.GetType()));
            if (handlers != null)
            {
                if (handlers.Count() != 1) throw new InvalidOperationException("cannot send command to more than one handler");
                ((ICommandHandler<T>)handlers.First()).Handle(command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var handlers = _serviceProvider.GetServices(typeof(IEventHandler<>).MakeGenericType(@event.GetType()));
            if (handlers == null) return;
            foreach (var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1.AsDynamic().Handle(@event));
            }
        }
    }
}
