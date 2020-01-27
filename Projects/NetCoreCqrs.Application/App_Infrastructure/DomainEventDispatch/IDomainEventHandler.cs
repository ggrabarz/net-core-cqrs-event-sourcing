using NetCoreCqrs.Domain.BuildingBlocks;
using System.Threading.Tasks;

namespace NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent domainEvent);
    }
}
