using NetCoreCqrs.Domain.BuildingBlocks;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent;
    }
}
