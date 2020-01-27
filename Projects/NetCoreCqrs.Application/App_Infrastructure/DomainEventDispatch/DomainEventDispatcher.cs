using NetCoreCqrs.Domain.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = GetServices(_serviceProvider, handlerType);
            if (handlers == null) return;
            foreach (dynamic handler in handlers)
            {
                await handler.HandleAsync((dynamic)domainEvent);
            }
        }

        private static IEnumerable<object> GetServices(IServiceProvider provider, Type type)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            return (IEnumerable<object>)provider.GetService(typeof(IEnumerable<>).MakeGenericType(type));
        }
    }
}
